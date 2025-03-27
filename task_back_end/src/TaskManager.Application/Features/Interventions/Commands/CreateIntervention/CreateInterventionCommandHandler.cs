using MediatR;
using TaskManager.Application.DTOs.Intervention;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Features.Interventions.Commands.CreateIntervention;

public class CreateInterventionCommandHandler() : IRequestHandler<CreateInterventionCommand, InterventionDto>
{

    public Task<InterventionDto> Handle(CreateInterventionCommand request, CancellationToken cancellationToken) => Task.FromResult(new InterventionDto());
}
