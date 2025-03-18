
namespace TaskManager.Domain.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Contract> ContractClientCompanies { get; set; } = new List<Contract>();

    public virtual ICollection<Contract> ContractTechnicianCompanies { get; set; } = new List<Contract>();

    public virtual ICollection<Technician> Technicians { get; set; } = new List<Technician>();
}
