using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Common.Models;
using TaskManager.Application.DTOs.Intervention;
using TaskManager.Application.Features.Interventions.Commands;
using TaskManager.Application.Features.Interventions.Queries;

namespace TaskManager.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
internal class InterventionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<PagedResponse<InterventionDto>>> GetAll([FromQuery] GetInterventionsQuery query)
    {
        // Validate and adjust page parameters
        if (query.PageNumber < 1)
        {
            query.PageNumber = 1;
        }

        if (query.PageSize < 1)
        {
            query.PageSize = 10;
        }

        PagedResponse<InterventionDto> result = await _mediator.Send(query);

        return Ok(result);
        
    }

    [HttpPost("/create")]
    public async Task<ActionResult<InterventionDto>> Create([FromBody] CreateInterventionCommand command){

        InterventionDto result = await _mediator.Send(command);

        return Ok(result);
    }
}
