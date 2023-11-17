using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Questions.Queries.GetById;

public class GetQuestionByIdHandler: IRequestHandler<GetQuestionByIdQuery, QuestionDto>
{
    private readonly IQuestionRepository repository;

    public GetQuestionByIdHandler(IQuestionRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<QuestionDto?> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var question = await repository.GetByIdAsync(request.Id);
        return !question.IsSuccess ? null : new QuestionDto
        {
            Id = question.Value.Id,
            QuizId = question.Value.QuizId,
            Description = question.Value.Description,
            Answer = question.Value.Answer,
            Options = question.Value.Options
        };
    }   
}