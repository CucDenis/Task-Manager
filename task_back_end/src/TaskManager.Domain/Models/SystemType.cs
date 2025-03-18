
namespace TaskManager.Domain.Models;

public partial class SystemType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<SubSystemType> SubSystemTypes { get; set; } = new List<SubSystemType>();
}
