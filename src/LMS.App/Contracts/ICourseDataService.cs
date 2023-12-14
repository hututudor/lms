using LMS.App.Services.Responses;
using LMS.App.ViewModels;

namespace LMS.App.Contracts
{
    public interface ICourseDataService
    {
        Task<List<CourseViewModel>> GetCoursesAsync();

        Task<ApiResponse<CourseViewModel>> CreateCourseAsync(CourseDto courseDto);
    }
}
