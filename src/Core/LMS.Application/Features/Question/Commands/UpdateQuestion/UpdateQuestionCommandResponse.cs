using LMS.Application.Features.Quizzes;
using LMS.Application.Responses;

namespace LMS.Application.Features.Questions.Commands.UpdateQuestion;

public class UpdateQuestionCommandResponse: BaseResponse
{
    public UpdateQuestionCommandResponse(): base()
    {
    }
    
    public QuestionDto Question { get; set; }
}