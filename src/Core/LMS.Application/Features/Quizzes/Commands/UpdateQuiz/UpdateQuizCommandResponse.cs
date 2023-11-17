using LMS.Application.Features.Quizzes;
using LMS.Application.Responses;

namespace LMS.Application.Features.Quizzes.Commands.UpdateQuiz;

public class UpdateQuizCommandResponse: BaseResponse
{
    public UpdateQuizCommandResponse(): base()
    {
    }
    
    public QuizDto Quiz { get; set; }
}