using LMS.Application.Features.Quiz.Commands.UpdateQuiz;
using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Quizzes.Commands.UpdateQuiz;

public class UpdateQuizCommandHandler: IRequestHandler<UpdateQuizCommand, UpdateQuizCommandResponse>
{
    private readonly IQuizRepository repository;

    public UpdateQuizCommandHandler(IQuizRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UpdateQuizCommandResponse> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await repository.GetByIdAsync(request.Id);
        if (!quiz.IsSuccess)
        {
            return new UpdateQuizCommandResponse
            {
                Message = $"Quiz with id {request.Id} not found",
                Success = false
            };
        }
        
        var updatedQuiz = Quiz.Update(quiz.Value, request.StepId, request.Name);
        if (!quiz.IsSuccess)
        {
            return new UpdateQuizCommandResponse
            {
                Message = updatedQuiz.Error,
                Success = false
            };
        }

        await repository.UpdateAsync(updatedQuiz.Value);
        
        return new UpdateQuizCommandResponse
        {
            Success = true,
            Quiz = new QuizDto()
            {
                Id = quiz.Value.Id,
                StepId = updatedQuiz.Value.StepId,
                Name = updatedQuiz.Value.Name
            }
        };
    }
}