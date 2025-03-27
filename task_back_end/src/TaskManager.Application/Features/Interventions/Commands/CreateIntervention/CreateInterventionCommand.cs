using MediatR;
using TaskManager.Application.DTOs.Intervention;

namespace TaskManager.Application.Features.Interventions.Commands;

public class CreateInterventionCommand : IRequest<InterventionDto> {
    public required string WorkPointAddress { get; set; }
    public required string Description { get; set; }

}
