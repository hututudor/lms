using LMS.Application.Responses;

namespace LMS.Application.Features.Questions.Queries.GetAll;

public class GetAllQuestionsQueryResponse
{
    public GetAllQuestionsQueryResponse()
    {
        
    }   
    public List<QuestionDto> Question { get; set; }
}