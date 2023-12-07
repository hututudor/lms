using LMS.Application.Features.Enrollments.Commands.CreateEnrollment;
using LMS.Application.Features.Enrollments.Commands.DeleteEnrollment;
using LMS.Application.Features.Enrollments.Commands.UpdateEnrollment;
using LMS.Application.Features.Enrollments.Queries.GetAllEnrollmentsByUserId;
using LMS.Application.Features.Enrollments.Queries.GetEnrollmentById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class EnrollmentController: ApiControllerBase
{
    [Authorize(Roles = "User")]
    [HttpGet("User/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId(Guid id)
    {
        var result = await Mediator.Send(new GetAllEnrollmentsByUserIdQuery(id));
        return Ok(result);
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetEnrollmentByIdQuery(id));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result); 
    }
    
    [Authorize(Roles = "User")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateEnrollmentCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    
    [Authorize(Roles = "User")]
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, UpdateEnrollmentCommand command)
    {
        command.Id = id;
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [Authorize(Roles = "User")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteEnrollmentCommand(id));
        return Ok(result);
    }

}