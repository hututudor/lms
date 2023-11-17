using LMS.Application.Features.Enrollments;
using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Steps.Commands.UpdateStep;

public class UpdateStepCommandHandler: IRequestHandler<UpdateStepCommand, UpdateStepCommandResponse>
{
    private readonly IStepRepository repository;

    public UpdateStepCommandHandler(IStepRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<UpdateStepCommandResponse> Handle(UpdateStepCommand request, CancellationToken cancellationToken)
    {
        var step = await repository.GetByIdAsync(request.Id);
        if (!step.IsSuccess)
        {
            return new UpdateStepCommandResponse
            {
                Message = $"Step with id {request.Id} not found",
                Success = false
            };
        }
        
        var updatedStep = Step.Update(step.Value, request.CourseId);
        if (!updatedStep.IsSuccess)
        {
            return new UpdateStepCommandResponse
            {
                Message = updatedStep.Error,
                Success = false
            };
        }

        await repository.UpdateAsync(updatedStep.Value);
        
        return new UpdateStepCommandResponse
        {
            Success = true,
            Step = new StepDto
            {
                Id = step.Value.Id,
                CourseId = updatedStep.Value.CourseId,
            }
        };
    }

}