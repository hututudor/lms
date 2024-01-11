using MediatR;

namespace LMS.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommand : IRequest<CreateCourseCommandResponse>
{ 
    public string Name { get; set; }
    public Guid UserId { get; set; }
}
