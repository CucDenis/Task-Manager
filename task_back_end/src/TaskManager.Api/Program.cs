using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Data;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Application.Abstractions.Data;
using TaskManager.Application.Features.Interventions.Queries.GetInterventions;
using TaskManager.Infrastructure.Services;
using TaskManager.Application.Abstractions.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
                                            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add CORS configuration
builder.Services.AddCors(options => options.AddPolicy("AllowFrontend",
                          policy => policy.WithOrigins("http://localhost:5173")
                                          .AllowAnyHeader()
                                          .AllowAnyMethod()
                                          .AllowCredentials()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found"))),
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    // Configure JWT bearer to read from cookies
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["auth_token"];
                            return Task.CompletedTask;
                        }
                    };
                });

// 🔑 Configure Role-Based Authorization
builder.Services.AddAuthorizationBuilder()
                .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
                .AddPolicy("ClientOnly", policy => policy.RequireRole("Client"))
                .AddPolicy("TechnicianOnly", policy => policy.RequireRole("Technician"))
                .AddPolicy("AdminOrClientOrTechnician", policy => policy.RequireRole("Admin", "Client", "Technician"))
                .AddPolicy("ClientOrTechnician", policy => policy.RequireRole("Client", "Technician"));

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly);
// builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()
//     .Where(assembly => assembly.FullName != null && 
//         (assembly.FullName.StartsWith("TaskManager.Application") || 
//          assembly.FullName.StartsWith("TaskManager.Api"))));

// Add MediatR
builder.Services.AddMediatR(configuration => 
                            configuration.RegisterServicesFromAssembly(typeof(GetInterventionsQuery).Assembly));

// Add JWT Service and Auth Service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add CORS middleware before authentication and authorization
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

await app.RunAsync();
