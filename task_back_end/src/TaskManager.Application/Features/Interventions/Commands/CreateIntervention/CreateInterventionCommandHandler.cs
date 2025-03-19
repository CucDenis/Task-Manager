// using MediatR;
// using TaskManager.Domain.Interfaces;
// using Microsoft.AspNetCore.Authorization;
// using TaskManager.Api.Controllers;

// namespace TaskManager.Application.Features.Interventions.Commands.CreateIntervention;

// [Authorize]
// public class CreateInterventionCommandHandler : BaseApiController, IRequestHandler<CreateInterventionCommand, InterventionDto>
// {
//     private readonly IUnitOfWork _unitOfWork;

//     public CreateInterventionCommandHandler(IUnitOfWork unitOfWork)
//     {
//         _unitOfWork = unitOfWork;
//     }

//     public Task<InterventionDto> Handle(CreateInterventionCommand request, CancellationToken cancellationToken)
//     {
//         var userId = GetCurrentUserId() ?? 
//             throw new UnauthorizedAccessException("User not authenticated");

//         var newIntervention = new InterventionDto {
//             WorkPointAddress = request.WorkPointAddress,
//             ClientId = userId
//         };

//         // ... continue with intervention creation
//     }
// }