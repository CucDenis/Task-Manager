

using MediatR;

public class CreateInterventionCommand : IRequest<InterventionDto> {
    public required string WorkPointAddress { get; set; }
    public required string Description { get; set; }

}