
namespace TaskManager.Domain.Models;

public partial class Contract
{
    public Guid Id { get; set; }

    public int? ClientCompanyId { get; set; }

    public int? TechnicianCompanyId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Company? ClientCompany { get; set; }

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();

    public virtual Company? TechnicianCompany { get; set; }
}
