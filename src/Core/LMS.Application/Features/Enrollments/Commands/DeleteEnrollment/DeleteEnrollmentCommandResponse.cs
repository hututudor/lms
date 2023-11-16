using LMS.Application.Responses;

namespace LMS.Application.Features.Enrollments.Commands.DeleteEnrollment;

public class DeleteEnrollmentCommandResponse: BaseResponse
{
    public DeleteEnrollmentCommandResponse() : base()
    {
        
    }
     
    public EnrollmentDto Enrollment { get; set; }
}