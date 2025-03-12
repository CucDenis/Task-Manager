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

    public async Task<(IJwtUser? user, string? token)> AuthenticateAsync(string email, string password, string userType)
    {
        IJwtUser? user = null;
        
        switch (userType.ToLower())
        {
            case "client":
                var clients = await _unitOfWork.Repository<Client>().GetAllAsync();
                user = clients.FirstOrDefault(c => 
                    c.Email?.Equals(email, StringComparison.OrdinalIgnoreCase) == true);
                break;

            case "technician":
                var technicians = await _unitOfWork.Repository<Technician>().GetAllAsync();
                user = technicians.FirstOrDefault(t => 
                    t.Email?.Equals(email, StringComparison.OrdinalIgnoreCase) == true);
                break;
        }

        if (user == null || !PasswordService.VerifyPassword(password, (user as dynamic).Password))
        {
            return (null, null);
        }

        var token = _jwtService.GenerateToken(user!);
        
        return (user, token);
    }
}
