using MediatR;

namespace LMS.Application.Features.Enrollments.Commands.DeleteEnrollment;

public record DeleteEnrollmentCommand(Guid Id): IRequest<DeleteEnrollmentCommandResponse>;