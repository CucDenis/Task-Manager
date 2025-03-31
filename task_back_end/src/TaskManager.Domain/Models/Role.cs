
namespace TaskManager.Domain.Models;

public partial class Role
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
