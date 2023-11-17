using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommandHandler: IRequestHandler<CreateQuizCommand, CreateQuizCommandResponse>
{
    private readonly IQuizRepository repository;

    public CreateQuizCommandHandler(IQuizRepository repository)
    {
        this.repository = repository;
    }

    public async Task<CreateQuizCommandResponse> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateQuizCommandValidator();
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
        {
            return new CreateQuizCommandResponse()
            {
                Success = false,
                ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }
        
        var quiz = Quiz.Create(request.Name, request.StepId);
        if (!quiz.IsSuccess)
        {
            return new CreateQuizCommandResponse()
            {
                Success = false,
                ValidationsErrors = new List<string> { quiz.Error }
            };
        }

        await repository.AddAsync(quiz.Value);

        return new CreateQuizCommandResponse()
        {
            Success = true,
            Quiz = new QuizDto()
            {
                Id = quiz.Value.Id,
                StepId = quiz.Value.StepId,
                Name = quiz.Value.Name,
            }
        };
    }
}