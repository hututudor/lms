using MediatR;

namespace LMS.Application.Features.Enrollments.Commands.UpdateEnrollment;

public class UpdateEnrollmentCommand: IRequest<UpdateEnrollmentCommandResponse>
{
    public UpdateEnrollmentCommand()
    {
    }
    
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime? CompletedAt { get; set; }
}