using LMS.Application.Responses;

namespace LMS.Application.Features.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommandResponse: BaseResponse
{
    public CreateQuizCommandResponse(): base()
    {
    }
    
    public QuizDto Quiz { get; set; }
}