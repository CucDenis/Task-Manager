using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class AuthenticationService(JwtService jwtService)
{
    private readonly JwtService _jwtService = jwtService;

    public Task<(User? user, string? token)> AuthenticateAsync(string password)
    {

        if (!PasswordService.VerifyPassword(password))
        {
            return (null, null);
        }

        string token = _jwtService.GenerateToken(user!);
        
        return (user, token);
    }
}
