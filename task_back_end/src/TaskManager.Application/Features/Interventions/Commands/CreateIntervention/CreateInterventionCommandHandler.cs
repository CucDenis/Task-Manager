using MediatR;
using TaskManager.Application.Abstractions.Data;
using TaskManager.Application.Abstractions.Repositories;
using TaskManager.Application.DTOs.Intervention;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Features.Interventions.Commands.CreateIntervention;

public class CreateInterventionCommandHandler(IUnitOfWork unitOfWork, IInterventionRepository interventionsRepository) : IRequestHandler<CreateInterventionCommand, InterventionDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private readonly IInterventionRepository _interventionsRepository = interventionsRepository;

    public async Task<InterventionDto> Handle(CreateInterventionCommand request, CancellationToken cancellationToken)
    {

        var intervention = new Intervention
        {
            Id = Guid.NewGuid(),
            ClientId = request.ClientId,
            TechnicianId = request.TechnicianId,
            Name = request.Name,
            LevelId = request.LevelId,
            Description = request.Description,
            Location = request.Location,
            CreatedAt = DateTime.UtcNow

        };

        await _interventionsRepository.AddAsync(intervention);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new InterventionDto { Id = intervention.Id, Name = intervention.Name };

    }
}
