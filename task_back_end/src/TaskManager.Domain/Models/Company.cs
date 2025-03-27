
namespace TaskManager.Domain.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Cui { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();

    public virtual ICollection<Contract> ContractClientCompanies { get; } = new List<Contract>();

    public virtual ICollection<Contract> ContractTechnicianCompanies { get; } = new List<Contract>();

    public virtual ICollection<Technician> Technicians { get; } = new List<Technician>();
}
