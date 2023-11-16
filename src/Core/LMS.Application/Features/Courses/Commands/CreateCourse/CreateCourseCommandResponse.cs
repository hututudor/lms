
using LMS.Application.Responses;

namespace LMS.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandResponse : BaseResponse
    {
        public CreateCourseCommandResponse() : base()
        {
            
        }

        public CourseDto Course { get; set; }
        
    }
}