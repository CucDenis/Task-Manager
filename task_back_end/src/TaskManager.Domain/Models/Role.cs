
namespace TaskManager.Domain.Models;

public partial class Role
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public virtual ICollection<User> Users { get; } = [];
}
