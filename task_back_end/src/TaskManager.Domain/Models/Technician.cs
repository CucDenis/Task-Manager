using System.Text.Json.Serialization;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Domain.Models;

public partial class Technician : IJwtUser
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    [JsonIgnore]
    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Signature { get; set; }

    public string? Status { get; set; }
}
