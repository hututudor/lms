using LMS.App.Services.Responses;
using LMS.App.ViewModels;

namespace LMS.App.Contracts
{
    public interface ILectureDataService
    {
        Task<List<LectureViewModel>> GetLectureAsync();

        Task<ApiResponse<LectureViewModel>> CreateLectureAsync(LectureDto lectureDto);
    }
}