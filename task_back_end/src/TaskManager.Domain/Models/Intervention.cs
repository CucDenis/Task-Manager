
namespace TaskManager.Domain.Models;

public partial class Intervention
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public Guid TechnicianId { get; set; }

    public string? Name { get; set; }

    public Guid? LevelId { get; set; }

    public required string Description { get; set; }

    public required Location Location { get; set; }

    public byte[]? ClientSignature { get; set; }

    public byte[]? TechnicianSignature { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();

    public virtual UrgencyLevel? Level { get; set; }

    public virtual Technician? Technician { get; set; }
}
