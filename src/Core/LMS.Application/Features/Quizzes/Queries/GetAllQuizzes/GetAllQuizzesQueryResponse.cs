using LMS.Application.Responses;

namespace LMS.Application.Features.Quizzes.Queries.GetAll;

public class GetAllQuizzesQueryResponse
{
    public List<QuizDto> Quizzes { get; set; }
}