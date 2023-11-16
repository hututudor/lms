using LMS.Application.Responses;

namespace LMS.Application.Features.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentCommandResponse: BaseResponse
{
    public CreateEnrollmentCommandResponse()
    {
    }

    public EnrollmentDto Enrollment { get; set; }
}