
namespace TaskManager.Domain.Models;

public partial class Availability
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Technician> Technicians { get; set; } = new List<Technician>();
}
