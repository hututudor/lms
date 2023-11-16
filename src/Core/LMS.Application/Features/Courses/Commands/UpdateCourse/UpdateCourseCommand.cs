using MediatR;

namespace LMS.Application.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommand: IRequest<UpdateCourseCommandResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
}