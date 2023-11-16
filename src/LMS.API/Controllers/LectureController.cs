﻿using LMS.Application.Features.Lectures.Commands.CreateLecture;
using LMS.Application.Features.Lectures.Commands.DeleteLecture;
using LMS.Application.Features.Lectures.Commands.UpdateLecture;
using LMS.Application.Features.Lectures.Queries.GetAll;
using LMS.Application.Features.Lectures.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class LectureController: ApiControllerBase
{
    [HttpGet("Lecture/Step/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid id)
    {
        var result = await Mediator.Send(new GetAllLecturesQuery(id));
        return Ok(result);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetLectureByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("Lecture/Step/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(Guid id, CreateLectureCommand command)
    {
        command.StepId = id;
        
        var result = await Mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    
    [HttpPut("Lecture/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, UpdateLectureCommand command)
    {
        command.Id = id;
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteLectureCommand(id));
        return Ok(result);
    }
}