using LMS.Application.Features.Lectures.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class LectureController: ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid id)
    {
        var result = await Mediator.Send(new GetAllLecturesQuery(id));
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

}