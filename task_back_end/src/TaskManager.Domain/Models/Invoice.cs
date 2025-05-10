
namespace TaskManager.Domain.Models;

public partial class Invoice
{
    public Guid Id { get; set; }

    public int? ContractId { get; set; }

    public int? InterventionId { get; set; }

    public string? Description { get; set; }

    public DateTime? EmmitingDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Contract? Contract { get; set; }

    public virtual Intervention? Intervention { get; set; }
}
