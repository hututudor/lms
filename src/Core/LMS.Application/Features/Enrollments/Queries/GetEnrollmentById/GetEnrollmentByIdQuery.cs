using MediatR;

namespace LMS.Application.Features.Enrollments.Queries.GetEnrollmentById;

public record GetEnrollmentByIdQuery(Guid Id): IRequest<EnrollmentDto?>;