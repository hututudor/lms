using MediatR;

namespace LMS.Application.Features.Courses.Commands.DeleteCourse;

public record DeleteCourseCommand(Guid Id): IRequest<DeleteCourseCommandResponse>;