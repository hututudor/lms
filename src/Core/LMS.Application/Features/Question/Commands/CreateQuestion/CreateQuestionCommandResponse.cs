using LMS.Application.Responses;

namespace LMS.Application.Features.Questions.Commands.CreateQuestion;

public class CreateQuestionCommandResponse: BaseResponse
{
    public CreateQuestionCommandResponse(): base()
    {
    }
    
    public QuestionDto Question { get; set; }
}