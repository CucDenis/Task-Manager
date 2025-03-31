using TaskManager.Application.Abstractions.Data;

namespace TaskManager.Infrastructure.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}
