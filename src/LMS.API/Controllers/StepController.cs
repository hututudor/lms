using LMS.Application.Features.Steps.Commands.CreateStep;
using LMS.Application.Features.Steps.Commands.DeleteStep;
using LMS.Application.Features.Steps.Commands.UpdateStep;
using LMS.Application.Features.Steps.Queries.GetAllStepsByCourseId;
using LMS.Application.Features.Steps.Queries.GetStepById;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class StepController: ApiControllerBase
{
    [HttpGet("Course/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByCourseId(Guid id)
    {
        var result = await Mediator.Send(new GetAllStepsByCourseIdQuery(id));
        return Ok(result);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetStepByIdQuery(id));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result); 
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateStepCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, UpdateStepCommand command)
    {
        command.Id = id;
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteStepCommand(id));
        return Ok(result);
    }
}