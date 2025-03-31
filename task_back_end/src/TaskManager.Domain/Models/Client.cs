
namespace TaskManager.Domain.Models;

public partial class Client
{
    public Guid Id { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public string? Phone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Intervention> Interventions { get; } = new List<Intervention>();

    public virtual User? User { get; set; }
}
