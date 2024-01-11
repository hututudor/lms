using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Questions.Commands.CreateQuestion;

public class CreateQuestionCommandHandler: IRequestHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
{
    private readonly IQuestionRepository repository;

    public CreateQuestionCommandHandler(IQuestionRepository repository)
    {
        this.repository = repository;
    }

    public async Task<CreateQuestionCommandResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateQuestionCommandValidator();
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
        {
            return new CreateQuestionCommandResponse()
            {
                Success = false,
                ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }
        
        var question = Question.Create(request.QuizId, request.Description, request.Answer, request.Options);
        if (!question.IsSuccess)
        {
            return new CreateQuestionCommandResponse()
            {
                Success = false,
                ValidationsErrors = new List<string> { question.Error }
            };
        }

        await repository.AddAsync(question.Value);

        return new CreateQuestionCommandResponse()
        {
            Success = true,
            Question = new QuestionDto()
            {
                Id = question.Value.Id,
                QuizId = question.Value.QuizId,
                Description = question.Value.Description,
                Answer = question.Value.Answer,
                Options = question.Value.Options,

            }
        };
    }
}