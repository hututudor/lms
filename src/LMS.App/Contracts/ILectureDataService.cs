using LMS.App.Services.Responses;
using LMS.App.ViewModels;

namespace LMS.App.Contracts
{
    public interface ILectureDataService
    {
        Task<List<LectureViewModel>> GetLecturesAsync(Guid stepId);
        
        Task<LectureViewModel> GetLectureAsync(Guid lectureId);


        Task<ApiResponse<LectureViewModel>> CreateLectureAsync(LectureDto lectureDto);
        
        Task<ApiResponse<LectureViewModel>> UpdateLectureAsync(LectureViewModel lectureViewModel);

    }
}