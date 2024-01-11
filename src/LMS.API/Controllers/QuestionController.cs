using LMS.Application.Features.Questions.Commands.CreateQuestion;
using LMS.Application.Features.Questions.Commands.DeleteQuestion;
using LMS.Application.Features.Questions.Commands.UpdateQuestion;
using LMS.Application.Features.Questions.Queries.GetAll;
using LMS.Application.Features.Questions.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class QuestionController: ApiControllerBase
{
    [Authorize(Roles = "User")]
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid id)
    {
        var result = await Mediator.Send(new GetAllQuestionsQuery(id));
        return Ok(result);
    }
    
    [Authorize(Roles = "User")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetQuestionByIdQuery(id));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(Guid id, CreateQuestionCommand command)
    {
        command.QuizId = id;
        
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
    public async Task<IActionResult> Update(Guid id, UpdateQuestionCommand command)
    {
        command.Id = id;
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [Authorize(Roles = "User")]
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteQuestionCommand(id));
        return Ok(result);
    }
}