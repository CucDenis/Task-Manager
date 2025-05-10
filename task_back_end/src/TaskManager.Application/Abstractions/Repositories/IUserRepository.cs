using TaskManager.Domain.Models;

namespace TaskManager.Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmail(string email);
    Task<bool> CheckEmailExists(string email);
}
