using LMS.Application.Responses;

namespace LMS.Application.Features.Questions.Commands.DeleteQuestion;

public class DeleteQuestionCommandResponse: BaseResponse
{
    public DeleteQuestionCommandResponse() : base()
    {
       
    }
    
    public QuestionDto Question { get; set; }
}