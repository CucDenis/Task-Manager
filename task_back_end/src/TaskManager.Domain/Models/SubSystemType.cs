
namespace TaskManager.Domain.Models;

public partial class SubSystemType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? SystemTypeId { get; set; }

    public virtual ICollection<SubSystemTypeExpertise> SubSystemTypeExpertises { get; } = new List<SubSystemTypeExpertise>();

    public virtual SystemType? SystemType { get; set; }
}
