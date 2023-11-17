using LMS.Application.Responses;

namespace LMS.Application.Features.Quizzes.Commands.DeleteQuiz;

public class DeleteQuizCommandResponse: BaseResponse
{
    public DeleteQuizCommandResponse() : base()
    {
       
    }
    
    public QuizDto Quiz { get; set; }
}