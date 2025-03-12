
namespace TaskManager.Domain.Models;

public partial class ClientContact
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public string? ContactName { get; set; }

    public virtual Client? Client { get; set; }
}
