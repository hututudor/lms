using FluentValidation;

namespace LMS.Application.Features.Steps.Commands.CreateStep;

public class CreateStepCommandValidator: AbstractValidator<CreateStepCommand>
{
    public CreateStepCommandValidator()
    {
        RuleFor(p => p.CourseId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}