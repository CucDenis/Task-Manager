namespace TaskManager.Domain.Models;

public partial class InterventionDocument
{
    public int Id { get; set; }

    public string? WorkPointAddress { get; set; }

    public string? ContactPerson { get; set; }

    public DateTime InterventionDate { get; set; }

    public string? Status { get; set; }

    public string? TimeInterval { get; set; }

    public int? TechnicianId { get; set; }
    public int? ClientId { get; set; }
    public string? Name { get; set; }

    public virtual Technician? Technician { get; set; }
    public virtual Client? Client { get; set; }
}
