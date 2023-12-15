using LMS.App.Services.Responses;
using LMS.App.ViewModels;

namespace LMS.App.Contracts
{
    public interface ILectureDataService
    {
        Task<List<LectureViewModel>> GetLecturesAsync();
        Task<LectureViewModel> GetLectureAsync(string stepId);

        Task<ApiResponse<LectureViewModel>> CreateLectureAsync(LectureDto lectureDto);
    }
}