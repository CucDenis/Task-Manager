

namespace TaskManager.Domain.Models;

public partial class InvoiceItem
{
    public int Id { get; set; }

    public int? InvoiceId { get; set; }

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? Vat { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
