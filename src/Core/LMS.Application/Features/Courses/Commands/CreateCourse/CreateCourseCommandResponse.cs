
using LMS.Application.Responses;
//using Xunit;

namespace LMS.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandResponse : BaseResponse
    {
        public CreateCourseCommandResponse() : base()
        {
            
        }

        public CreateCourseDto Course { get; set; }
        
    }
}