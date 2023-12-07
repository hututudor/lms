using LMS.Application.Features.Quizzes.Commands.CreateQuiz;
using LMS.Application.Features.Quizzes.Commands.DeleteQuiz;
using LMS.Application.Features.Quizzes.Commands.UpdateQuiz;
using LMS.Application.Features.Quizzes.Queries.GetAll;
using LMS.Application.Features.Quizzes.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class QuizController: ApiControllerBase
{
    [Authorize(Roles = "User")]
    [HttpGet("Step/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid id)
    {
        var result = await Mediator.Send(new GetAllQuizzesQuery(id));
        return Ok(result);
    }
    
    [Authorize(Roles = "User")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetQuizByIdQuery(id));
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("Step")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(Guid id, CreateQuizCommand command)
    {
        command.StepId = id;
        
        var result = await Mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, UpdateQuizCommand command)
    {
        command.Id = id;
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteQuizCommand(id));
        return Ok(result);
    }
}