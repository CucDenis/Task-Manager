
using TaskManager.Domain.Models;

namespace TaskManager.Application.Abstractions.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}
