

namespace TaskManager.Domain.Models;

public partial class Contract
{
    public int Id { get; set; }

    public int? OfferId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool? FreeInterventions { get; set; }

    public string? Signatures { get; set; }

    public virtual Offer? Offer { get; set; }
}
