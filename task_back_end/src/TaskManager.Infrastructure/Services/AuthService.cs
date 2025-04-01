using TaskManager.Api.DTOs.Auth;
using TaskManager.Application.Abstractions.Data;
using TaskManager.Application.Abstractions.Repositories;
using TaskManager.Application.Abstractions.Services;
using TaskManager.Application.DTOs.Auth;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.Services;

public class AuthService(IUserRepository userRepository ,IUnitOfWork unitOfWork) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<User?> AuthenticateUser(LoginDto loginDto)
    {
        User? userFound = await _userRepository.GetByEmail(loginDto.Email);

        if (userFound == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, userFound.Password))
        {
            return null;
        }

        return userFound;

    }
    public async Task<User?> RegisterUser(RegisterDto registerDto)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            RoleId = registerDto.RoleId,
            CreatedAt = DateTime.Now,
            Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            Cnp = registerDto.Cnp
        };

        await _userRepository.AddAsync(newUser);

        await _unitOfWork.SaveChangesAsync();

        return newUser;
    }
}
