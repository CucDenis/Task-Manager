
namespace TaskManager.Domain.Models;

public partial class Technician
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public string? Phone { get; set; }

    public int? AvailabilityId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Availability? Availability { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Intervention> Interventions { get; } = new List<Intervention>();

    public virtual ICollection<SubSystemTypeExpertise> SubSystemTypeExpertises { get; } = new List<SubSystemTypeExpertise>();

    public virtual User? User { get; set; }
}
