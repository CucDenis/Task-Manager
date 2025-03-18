using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Common.Models;
using TaskManager.Application.Features.Interventions.Queries.GetInterventions;

namespace TaskManager.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InterventionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InterventionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<InterventionDto>>> GetAll([FromQuery] GetInterventionsQuery query)
    {
        // Validate and adjust page parameters
        if (query.PageNumber < 1) query.PageNumber = 1;
        if (query.PageSize < 1) query.PageSize = 10;
        
        var result = await _mediator.Send(query);

        return Ok(result);
        
    }

    [HttpPost("/create")]
    public async Task<ActionResult<InterventionDto>> Create([FromBody] CreateInterventionCommand command){
        
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
