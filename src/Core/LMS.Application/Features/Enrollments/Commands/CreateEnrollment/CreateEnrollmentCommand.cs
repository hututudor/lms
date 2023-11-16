using MediatR;

namespace LMS.Application.Features.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentCommand: IRequest<CreateEnrollmentCommandResponse>
{
    public CreateEnrollmentCommand()
    {
    }
    
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
}