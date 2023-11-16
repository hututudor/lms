using MediatR;

namespace LMS.Application.Features.Steps.Commands.UpdateStep;

public class UpdateStepCommand: IRequest<UpdateStepCommandResponse>
{
    public UpdateStepCommand()
    {
    }
    
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}