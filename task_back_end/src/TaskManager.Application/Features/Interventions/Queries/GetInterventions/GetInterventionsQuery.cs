using MediatR;
using TaskManager.Application.Common.Models;
using TaskManager.Application.DTOs.Intervention;

namespace TaskManager.Application.Features.Interventions.Queries.GetInterventions;


public class GetInterventionsQuery : IRequest<PagedResponse<InterventionDto>>
{
    public string? InterventionName { get; set; }
    public string? TechnicianName { get; set; }
    public string? ClientName { get; set; }
    public string? InterventionDate { get; set; }
    public int PageNumber { get; set; } = 1; 
    public int PageSize { get; set; } = 10;
}
