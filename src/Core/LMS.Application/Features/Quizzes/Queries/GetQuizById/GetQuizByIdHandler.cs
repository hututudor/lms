using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Quizzes.Queries.GetById;

public class GetQuizByIdHandler: IRequestHandler<GetQuizByIdQuery, QuizDto>
{
    private readonly IQuizRepository repository;

    public GetQuizByIdHandler(IQuizRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<QuizDto?> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
    {
        var quiz = await repository.GetByIdAsync(request.Id);
        return !quiz.IsSuccess ? null : new QuizDto
        {
            Id = quiz.Value.Id,
            StepId = quiz.Value.StepId,
            Name = quiz.Value.Name
        };
    }   
}