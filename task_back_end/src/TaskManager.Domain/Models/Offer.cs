
namespace TaskManager.Domain.Models;

public partial class Offer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateOnly? ValidityPeriod { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? ClientCompany { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = [];

    public virtual ICollection<OfferDevice> OfferDevices { get; set; } = [];

    public virtual ICollection<OfferMaintenanceSystem> OfferMaintenanceSystems { get; set; } = [];
}
