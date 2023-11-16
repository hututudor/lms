using MediatR;

namespace LMS.Application.Features.Enrollments.Queries.GetAllEnrollmentsByUserId;

public record GetAllEnrollmentsByUserIdQuery(Guid UserId) : IRequest<GetAllEnrollmentsByUserIdQueryResponse>;
