using MediatR;
using TaskManager.Application.Common.Models;

namespace TaskManager.Application.Features.Interventions.Queries;

public class GetInterventionsQuery : IRequest<PagedResponse<InterventionDto>>
{
    public string? InterventionName { get; set; }
    public string? TechnicianName { get; set; }
    public string? ClientName { get; set; }
    public string? InterventionDate { get; set; }
    public int PageNumber { get; set; } = 1; 
    public int PageSize { get; set; } = 10;
}

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
