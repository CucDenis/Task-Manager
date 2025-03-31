
namespace TaskManager.Domain.Models;

public partial class SystemType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<SubSystemType> SubSystemTypes { get; } = new List<SubSystemType>();
}
