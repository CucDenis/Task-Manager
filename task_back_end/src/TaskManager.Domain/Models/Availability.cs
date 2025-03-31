
namespace TaskManager.Domain.Models;

public partial class Availability
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Technician> Technicians { get; } = new List<Technician>();
}
