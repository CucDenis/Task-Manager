using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class InterventionRepository(AppDbContext context) : Repository<Intervention>(context)
{
}
