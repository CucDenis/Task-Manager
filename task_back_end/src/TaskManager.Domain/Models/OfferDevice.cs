

namespace TaskManager.Domain.Models;

public partial class OfferDevice
{
    public int Id { get; set; }

    public int? OfferId { get; set; }

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public string? Category { get; set; }

    public string? SubCategory { get; set; }

    public string? SystemType { get; set; }

    public virtual Offer? Offer { get; set; }
}
