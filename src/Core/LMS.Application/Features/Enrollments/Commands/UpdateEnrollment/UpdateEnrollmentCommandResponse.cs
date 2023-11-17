using LMS.Application.Responses;

namespace LMS.Application.Features.Enrollments.Commands.UpdateEnrollment;

public class UpdateEnrollmentCommandResponse: BaseResponse
{
    public UpdateEnrollmentCommandResponse() : base()
    {
        
    }
     
    public EnrollmentDto Enrollment { get; set; }
}