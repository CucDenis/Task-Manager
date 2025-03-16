using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Services;
using TaskManager.Api.DTOs.Auth;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using FluentValidation;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtService _jwtService;
    private readonly IValidator<RegisterDto> _validator;

    public ClientsController(IUnitOfWork unitOfWork, JwtService jwtService, IValidator<RegisterDto> validator)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _validator = validator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
    {
        var validationResult = _validator.Validate(registerDto);

        if (!validationResult.IsValid)
        {
            return ValidationProblem(
                new ValidationProblemDetails(validationResult.ToDictionary())
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Failed",
                    Detail = "One or more validation errors occurred.",
                    Instance = HttpContext.Request.Path

                }
            );

        }

        try
        {
            var clients = await _unitOfWork.Repository<Client>().GetAllAsync();

            if (clients.Any(c => c.Email?.Equals(registerDto.Email, StringComparison.OrdinalIgnoreCase) == true))
            {
                return BadRequest(new { message = "Email already exists" });
            }

            var client = new Client
            {
                Name = registerDto.Name,
                Email = registerDto.Email.ToLower(),
                Password = PasswordService.HashPassword(registerDto.Password),
                CuiCnp = registerDto.CuiCnp

            };

            await _unitOfWork.Repository<Client>().AddAsync(client);

            var result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
            {
                throw new Exception("Failed to save client");

            }

            var token = _jwtService.GenerateToken(client);
            
            // Set HttpOnly cookie
            Response.Cookies.Append("auth_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)

            });

            var response = new AuthResponseDto
            {
                Email = client.Email ?? string.Empty,
                FullName = client.Name ?? string.Empty
                
            };

            return Ok(response);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Registration error: {ex}");

            return StatusCode(500, new { message = ex.Message });

        }

    }


}
