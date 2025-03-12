

namespace TaskManager.Domain.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public string? IssuingCompany { get; set; }

    public string? ClientCompany { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Total { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? PaymentDue { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = [];
}
