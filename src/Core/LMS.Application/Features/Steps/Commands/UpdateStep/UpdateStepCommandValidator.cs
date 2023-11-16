using FluentValidation;

namespace LMS.Application.Features.Steps.Commands.UpdateStep;

public class UpdateStepCommandValidator: AbstractValidator<UpdateStepCommand>
{
    public UpdateStepCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        
        RuleFor(p => p.CourseId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}