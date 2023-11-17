using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Questions.Commands.DeleteQuestion;

public class DeleteQuestionCommandHandler: IRequestHandler<DeleteQuestionCommand, DeleteQuestionCommandResponse>
{
    private readonly IQuestionRepository repository;

    public DeleteQuestionCommandHandler(IQuestionRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DeleteQuestionCommandResponse> Handle(DeleteQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var question = await repository.GetByIdAsync(request.Id);
        if (!question.IsSuccess)
        {
            return new DeleteQuestionCommandResponse
            {
                Message = $"Question with id {request.Id} not found",
                Success = false
            };
        }

        await repository.DeleteAsync(request.Id);
        return new DeleteQuestionCommandResponse
        {
            Success = true
        };
    }
}