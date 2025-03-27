using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services;

public class AuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtService _jwtService;

    public AuthenticationService(IUnitOfWork unitOfWork, JwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<(User? user, string? token)> AuthenticateAsync(string email, string password, string userType)
    {
        User? user = null;

        switch (userType.ToUpperInvariant())
        {
            case "CLIENT":
                IEnumerable<User> clients = await _unitOfWork.Repository<User>().GetAllAsync();
                user = clients.FirstOrDefault(c =>
                    c.Email?.Equals(email, StringComparison.OrdinalIgnoreCase) == true);
                break;

            case "TECHNICIAN":
                IEnumerable<User> technicians = await _unitOfWork.Repository<User>().GetAllAsync();
                user = technicians.FirstOrDefault(t =>
                    t.Email?.Equals(email, StringComparison.OrdinalIgnoreCase) == true);
                break;
            default:
                break;
        }

        if (user == null || !PasswordService.VerifyPassword(password, (user as dynamic).Password))
        {
            return (null, null);
        }

        string token = _jwtService.GenerateToken(user!);
        
        return (user, token);
    }
}
