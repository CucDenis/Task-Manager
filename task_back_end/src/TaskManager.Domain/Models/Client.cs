using TaskManager.Domain.Interfaces;

namespace TaskManager.Domain.Models;

public partial class Client : IJwtUser
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CuiCnp { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public virtual ICollection<ClientContact> ClientContacts { get; set; } = [];

    // Implementation of IJwtUser
    string? IJwtUser.FirstName => Name;
    string? IJwtUser.LastName => string.Empty;
}
