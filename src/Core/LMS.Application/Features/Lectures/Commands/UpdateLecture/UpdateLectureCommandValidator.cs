using FluentValidation;

namespace LMS.Application.Features.Lectures.Commands.UpdateLecture;

public class UpdateLectureCommandValidator: AbstractValidator<UpdateLectureCommand>
{
    public UpdateLectureCommandValidator()
    {
        RuleFor(p => p.StepId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        
        RuleFor(p => p.Url)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}