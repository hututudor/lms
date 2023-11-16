using FluentValidation;
using LMS.Application.Features.Courses.Commands.UpdateCourse;

namespace LMS.Application.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandValidator: AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(p => p.UserId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}