public class InterventionDto
{
    public int Id { get; set; }
    public string WorkPointAddress { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string? InterventionDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string TimeInterval { get; set; } = string.Empty;
    public string TechnicianName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
}