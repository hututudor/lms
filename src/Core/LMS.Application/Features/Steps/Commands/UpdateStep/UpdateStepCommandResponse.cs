using LMS.Application.Responses;

namespace LMS.Application.Features.Steps.Commands.UpdateStep;

public class UpdateStepCommandResponse: BaseResponse
{
    public UpdateStepCommandResponse(): base()
    {
    }
    
    public StepDto Step { get; set; }
}