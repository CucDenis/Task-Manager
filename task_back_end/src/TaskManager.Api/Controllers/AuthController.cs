using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTOs.Auth;
using FluentValidation;
using TaskManager.Domain.Models;
using TaskManager.Application.DTOs.Auth;
using FluentValidation.Results;
using TaskManager.Application.Abstractions.Repositories;
using TaskManager.Infrastructure.Services;
using TaskManager.Application.Abstractions.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
internal class AuthController(IAuthService authService,
    IUserRepository userRepository, JwtService jwtService, IValidator<RegisterDto> validator) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly JwtService _jwtService = jwtService;
    private readonly IValidator<RegisterDto> _validator = validator;

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(registerDto);

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

            if (await _userRepository.CheckEmailExists(registerDto.Email))
            {
                return BadRequest(new { message = "Email already exists" });
            }

            User? newUser = await _authService.RegisterUser(registerDto) ?? throw new Exception(" User not registered. Something went wrong.");

            string token = _jwtService.GenerateToken(newUser);

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
                Email = newUser.Email ?? string.Empty,
                FullName = $"{newUser.FirstName} {newUser.LastName}"

            };

            return Ok(response);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Registration error: {ex}");

            return StatusCode(500, new { message = ex.Message });

        }

    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            User? user = await _authService.AuthenticateUser(loginDto);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });

            }

            string token = _jwtService.GenerateToken(user);

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
}
