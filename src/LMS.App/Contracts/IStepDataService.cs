using LMS.App.Services.Responses;
using LMS.App.ViewModels;

namespace LMS.App.Contracts
{
    public interface IStepDataService
    {
        Task<List<StepViewModel>> GetStepsAsync(Guid courseId);

        Task<ApiResponse<StepViewModel>> CreateStepAsync(StepDto stepDto);
        Task<ApiResponse<StepViewModel>> DeleteStepAsync(Guid stepId);
    }
}