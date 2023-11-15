using LMS.Application.Features.Courses.Commands.CreateCourse;
using MediatR;

namespace LMS.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<CreateCourseCommandResponse>
    {
        public string Name { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
    }
}