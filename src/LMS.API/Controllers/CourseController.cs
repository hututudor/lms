using LMS.Application.Features.Courses.Commands.CreateCourse;
using LMS.Application.Features.Courses.Commands.DeleteCourse;
using LMS.Application.Features.Courses.Commands.UpdateCourse;
using LMS.Application.Features.Courses.Queries;
using LMS.Application.Features.Courses.Queries.GetCourseById;
using LMS.Application.Features.Lectures.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LMS.API.Controllers
{
    public class CourseController : ApiControllerBase
    {
        [Authorize(Roles = "Admin,User")]
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
        
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var result = await Mediator.Send(new GetAllCoursesQuery(id));
            return Ok(result);
        }
    
        [Authorize(Roles = "User")]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetCourseByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [Authorize(Roles = "User")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, UpdateCourseCommand command)
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
            var result = await Mediator.Send(new DeleteCourseCommand(id));
            return Ok(result);
        }
    }
}

