using LMS.App.Services.Responses;
using LMS.App.ViewModels;

namespace LMS.App.Contracts
{
    public interface IQuizDataService
    {
        Task<List<QuizViewModel>> GetQuizzesAsync(Guid stepId);

        Task<QuizViewModel> GetQuizAsync(Guid quizId);

        Task<ApiResponse<QuizViewModel>> CreateQuizAsync(QuizDto quizDto);
        
        Task<ApiResponse<QuizViewModel>> UpdateQuizAsync(QuizViewModel quizViewModel);
        
        Task<ApiResponse<QuizViewModel>> DeleteQuizAsync(Guid quizId);
    }
}