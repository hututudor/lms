using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Questions.Queries.GetAll;

public class GetAllQuestionsQueryHandler: IRequestHandler<GetAllQuestionsQuery, GetAllQuestionsQueryResponse>
{
    private readonly IQuestionRepository repository;

    public GetAllQuestionsQueryHandler(IQuestionRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<GetAllQuestionsQueryResponse> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await repository.GetAllByQuizId(request.QuizId);
        var questionsDto = questions.Value.Select(question => new QuestionDto()
        {
            Id = question.Id,
            QuizId = question.QuizId,
            Description = question.Description,
            Answer = question.Answer,
            Options = question.Options
        }).ToList();

        return new GetAllQuestionsQueryResponse
        {
            Question = questionsDto
        };
    }
}