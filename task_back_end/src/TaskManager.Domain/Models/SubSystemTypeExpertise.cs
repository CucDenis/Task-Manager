
namespace TaskManager.Domain.Models;

public partial class SubSystemTypeExpertise
{
    public Guid Id { get; set; }

    public int? TechnicianId { get; set; }

    public int? SubSystemTypeId { get; set; }

    public virtual SubSystemType? SubSystemType { get; set; }

    public virtual Technician? Technician { get; set; }
}
