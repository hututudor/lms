using FluentValidation;

namespace LMS.Application.Features.Lectures.Commands.CreateLecture;

public class CreateLectureCommandValidator: AbstractValidator<CreateLectureCommand>
{
    public CreateLectureCommandValidator()
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