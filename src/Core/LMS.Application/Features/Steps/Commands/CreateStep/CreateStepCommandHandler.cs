using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Steps.Commands.CreateStep;

public class CreateStepCommandHandler: IRequestHandler<CreateStepCommand, CreateStepCommandResponse>
{
    private readonly IStepRepository repository;

    public CreateStepCommandHandler(IStepRepository repository)
    {
        this.repository = repository;
    }

    public async Task<CreateStepCommandResponse> Handle(CreateStepCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateStepCommandValidator();
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
        {
            return new CreateStepCommandResponse
            {
                Success = false,
                ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var step = Step.Create(request.CourseId);
        if (!step.IsSuccess)
        {
            return new CreateStepCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { step.Error }
            };
        }

        await repository.AddAsync(step.Value);

        return new CreateStepCommandResponse
        {
            Success = true,
            Step = new StepDto()
            {
                Id = step.Value.Id,
                CourseId = step.Value.CourseId,
            }
        };
    }

}