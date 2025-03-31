using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class InterventionsRepository : Repository<Intervention>
{
    public InterventionsRepository(AppDbContext context) : base(context)
    {
    }
}
