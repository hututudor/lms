using LMS.Application.Responses;

namespace LMS.Application.Features.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandResponse: BaseResponse
{
    public DeleteCourseCommandResponse() : base()
    {
       
    }
    
    public CourseDto Course { get; set; }
}