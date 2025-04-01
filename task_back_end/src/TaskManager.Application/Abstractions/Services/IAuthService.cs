using TaskManager.Api.DTOs.Auth;
using TaskManager.Application.DTOs.Auth;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Abstractions.Services;

public interface IAuthService
{
    Task<User?> AuthenticateUser(LoginDto loginDto);
    Task<User?> RegisterUser(RegisterDto registerDto);
}
