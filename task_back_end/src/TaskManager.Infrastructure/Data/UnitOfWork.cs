using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private Dictionary<Type, object> _repositories;
    private bool _disposed;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = [];
    }

    public IRepository<T> Repository<T>() where T : class
    {
        if (_repositories.TryGetValue(typeof(T), out object? repository))
        {
            return (IRepository<T>)repository;
        }

        repository = new Repository<T>(_context);
        _repositories.Add(typeof(T), repository);
        return (IRepository<T>)repository;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }
}
