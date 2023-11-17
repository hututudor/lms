using FluentValidation;

namespace LMS.Application.Features.Enrollments.Commands.UpdateEnrollment;

public class UpdateEnrollmentCommandValidator: AbstractValidator<UpdateEnrollmentCommand>
{
    public UpdateEnrollmentCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        
        RuleFor(p => p.UserId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.CourseId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}