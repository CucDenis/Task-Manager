
namespace TaskManager.Domain.Models;

public partial class User
{
    public required Guid Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required Guid RoleId { get; set; }

    public string? RefreshToken { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required string Password { get; set; }

    public required string Cnp { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Technician> Technicians { get; } = new List<Technician>();
}
