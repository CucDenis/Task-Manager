
namespace TaskManager.Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int? RoleId { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Password { get; set; }

    public string? Cnp { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Technician> Technicians { get; } = new List<Technician>();
}
