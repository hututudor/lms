using LMS.Application.Features.Courses.Commands.CreateCourse;
using Microsoft.AspNetCore.Mvc;
namespace LMS.API.Controllers
{
    public class CourseController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateCourseCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}

