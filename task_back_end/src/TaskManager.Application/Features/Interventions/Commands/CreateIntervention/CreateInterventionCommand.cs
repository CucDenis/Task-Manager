using MediatR;
using TaskManager.Application.DTOs.Intervention;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Features.Interventions.Commands.CreateIntervention;

public class CreateInterventionCommand : IRequest<InterventionDto> {

    public Guid ClientId { get; set; }

    public Guid TechnicianId { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid LevelId { get; set; }

    public required string Description { get; set; }

    public required Location Location { get; set; }

}
