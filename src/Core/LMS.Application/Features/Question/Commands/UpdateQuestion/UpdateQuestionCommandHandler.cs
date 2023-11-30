using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Questions.Commands.UpdateQuestion;

public class UpdateQuestionCommandHandler: IRequestHandler<UpdateQuestionCommand, UpdateQuestionCommandResponse>
{
    private readonly IQuestionRepository repository;

    public UpdateQuestionCommandHandler(IQuestionRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UpdateQuestionCommandResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        
        var validator = new UpdateQuestionCommandValidator();
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
        {
            return new UpdateQuestionCommandResponse()
            {
                Success = false,
                ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }
        var question = await repository.GetByIdAsync(request.Id);
        if (!question.IsSuccess)
        {
            return new UpdateQuestionCommandResponse
            {
                Message = $"Question with id {request.Id} not found",
                Success = false
            };
        }
        
        var updatedQuestion = Question.Update(question.Value, request.QuizId, request.Description, request.Answer, request.Options);
        if (!question.IsSuccess)
        {
            return new UpdateQuestionCommandResponse
            {
                Message = updatedQuestion.Error,
                Success = false
            };
        }

        await repository.UpdateAsync(updatedQuestion.Value);
        
        return new UpdateQuestionCommandResponse
        {
            Success = true,
            Question = new QuestionDto()
            {
                Id = question.Value.Id,
                QuizId = updatedQuestion.Value.QuizId,
                Description = updatedQuestion.Value.Description,
                Answer = updatedQuestion.Value.Answer,
                Options = updatedQuestion.Value.Options
            }
        };
    }
}