using LMS.Application.Responses;

namespace LMS.Application.Features.Steps.Commands.DeleteStep;

public class DeleteStepCommandResponse: BaseResponse
{
    public DeleteStepCommandResponse()
    {
    }
    
    public StepDto Step { get; set; }
}