﻿using LMS.Application.Responses;

namespace LMS.Application.Features.Steps.Commands.CreateStep;

public class CreateStepCommandResponse: BaseResponse
{
    public CreateStepCommandResponse()
    {
    }

    public StepDto Step { get; set; }
}