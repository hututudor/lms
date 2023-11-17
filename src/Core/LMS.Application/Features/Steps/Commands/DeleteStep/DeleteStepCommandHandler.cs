using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Steps.Commands.DeleteStep;

public class DeleteStepCommandHandler: IRequestHandler<DeleteStepCommand, DeleteStepCommandResponse>
{
    private readonly IStepRepository repository;

    public DeleteStepCommandHandler(IStepRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DeleteStepCommandResponse> Handle(DeleteStepCommand request,
        CancellationToken cancellationToken)
    {
        var step = await repository.GetByIdAsync(request.Id);
        if (!step.IsSuccess)
        {
            return new DeleteStepCommandResponse
            {
                Message = $"Step with id {request.Id} not found",
                Success = false
            };
        }

        await repository.DeleteAsync(request.Id);
        return new DeleteStepCommandResponse
        {
            Success = true
        };
    }
}