
namespace TaskManager.Domain.Models;

public partial class Intervention
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public int? TechnicianId { get; set; }

    public string? Name { get; set; }

    public int? LevelId { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public byte[]? ClientSignature { get; set; }

    public byte[]? TechnicianSignature { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual UrgencyLevel? Level { get; set; }

    public virtual Technician? Technician { get; set; }
}
