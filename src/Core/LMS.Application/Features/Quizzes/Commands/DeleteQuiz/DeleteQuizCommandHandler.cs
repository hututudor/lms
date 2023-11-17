using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Quizzes.Commands.DeleteQuiz;

public class DeleteQuizCommandHandler: IRequestHandler<DeleteQuizCommand, DeleteQuizCommandResponse>
{
    private readonly IQuizRepository repository;

    public DeleteQuizCommandHandler(IQuizRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DeleteQuizCommandResponse> Handle(DeleteQuizCommand request,
        CancellationToken cancellationToken)
    {
        var quiz = await repository.GetByIdAsync(request.Id);
        if (!quiz.IsSuccess)
        {
            return new DeleteQuizCommandResponse
            {
                Message = $"Quiz with id {request.Id} not found",
                Success = false
            };
        }

        await repository.DeleteAsync(request.Id);
        return new DeleteQuizCommandResponse
        {
            Success = true
        };
    }
}