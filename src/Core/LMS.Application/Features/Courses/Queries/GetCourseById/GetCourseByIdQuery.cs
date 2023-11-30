namespace LMS.Application.Features.Courses.Queries.GetCourseById;
using MediatR;
public record GetCourseByIdQuery(Guid Id) : IRequest<CourseDto?>;