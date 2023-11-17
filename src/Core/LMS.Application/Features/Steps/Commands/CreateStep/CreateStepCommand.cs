using MediatR;

namespace LMS.Application.Features.Steps.Commands.CreateStep;

public class CreateStepCommand: IRequest<CreateStepCommandResponse>
{
    public CreateStepCommand()
    {
    }
    
    public Guid CourseId { get; set; }
}