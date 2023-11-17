using MediatR;

namespace LMS.Application.Features.Steps.Queries.GetAllStepsByCourseId;

public record GetAllStepsByCourseIdQuery(Guid CourseId) : IRequest<GetAllStepsByCourseIdQueryResponse>;