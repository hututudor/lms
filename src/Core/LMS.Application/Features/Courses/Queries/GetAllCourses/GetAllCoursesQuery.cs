using MediatR;
namespace LMS.Application.Features.Courses.Queries;

public record GetAllCoursesQuery(Guid StepId) : IRequest<GetAllCoursesQueryResponse>;