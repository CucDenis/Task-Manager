
namespace TaskManager.Domain.Models;

public partial class UrgencyLevel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Intervention> Interventions { get; } = new List<Intervention>();
}
