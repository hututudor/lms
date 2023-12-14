using LMS.App.Services.Responses;
using LMS.App.ViewModels;

namespace LMS.App.Contracts
{
    public interface ICourseDataService
    {
        Task<List<CourseViewModel>> GetCoursesAsync();
        Task<CourseViewModel> GetCourseAsync(string courseId);

        Task<ApiResponse<CourseViewModel>> CreateCourseAsync(CourseDto courseDto);
        
        Task<ApiResponse<CourseViewModel>> UpdateCourseAsync(CourseViewModel courseDto);
    }
}
