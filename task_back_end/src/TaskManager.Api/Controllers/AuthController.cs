using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Services;
using TaskManager.Api.DTOs.Auth;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(AuthenticationService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto loginDto)
    {
        try
        {
            var (user, token) = await _authService.AuthenticateAsync(
                loginDto.Email, 
                loginDto.Password, 
                loginDto.UserType
            );

            if (user == null || token == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Set HttpOnly cookie
            Response.Cookies.Append("auth_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(new AuthResponseDto
            {
                Email = user.Email ?? string.Empty,
                FullName = $"{user.FirstName} {user.LastName}",
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("logout")]
    public ActionResult Logout()
    {
        Response.Cookies.Delete("auth_token", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
        
        return Ok(new { message = "Logged out successfully" });
    }

    [HttpGet("check-auth")]
    public ActionResult<AuthStatusResponse> CheckAuth()
    {
        try
        {
            var token = Request.Cookies["auth_token"];
            if (string.IsNullOrEmpty(token))
            {
                return Ok(new AuthStatusResponse 
                { 
                    IsAuthenticated = false 
                });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found"));
            
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var userType = principal.FindFirst("UserType")?.Value;

                return Ok(new AuthStatusResponse 
                { 
                    IsAuthenticated = true,
                });
            }
            catch
            {

                Response.Cookies.Delete("auth_token");
                return Ok(new AuthStatusResponse 
                { 
                    IsAuthenticated = false 
                });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}

public class AuthStatusResponse
{
    public bool IsAuthenticated { get; set; }
    
}
