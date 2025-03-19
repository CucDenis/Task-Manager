public class InterventionDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ClientName { get; set; }

    public string? InterventionDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Deadline { get; set; } = string.Empty;

    public string? TechnicianName { get; set; }

    public string? UrgencyLevel { get; set; }

    public byte[]? ClientSignature { get; set; }

    public byte[]? TechnicianSignature { get; set; }

}