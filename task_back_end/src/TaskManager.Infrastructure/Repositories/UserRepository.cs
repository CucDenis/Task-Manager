using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions.Repositories;
using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{

    public async Task<User?> GetByEmail(string email)
    {
        User? user = await Context.Users.Include(u => u.Role).FirstOrDefaultAsync(user => user.Email == email);

        return user;
    }


    public async Task<bool> CheckEmailExists(string email)
    {
        User? existingUser = await Context.Users.FirstOrDefaultAsync(user => user.Email == email);

        return existingUser != null;
    }

}
