using LMS.Application.Responses;

namespace LMS.Application.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandResponse: BaseResponse
{
    public UpdateCourseCommandResponse(): base()
    {
    }
    
    public CourseDto Course { get; set; }
}