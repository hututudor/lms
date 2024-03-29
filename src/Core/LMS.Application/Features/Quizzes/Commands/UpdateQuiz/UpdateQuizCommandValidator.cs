using FluentValidation;

namespace LMS.Application.Features.Quizzes.Commands.UpdateQuiz;

public class UpdateQuizCommandValidator: AbstractValidator<UpdateQuizCommand>
{
    public UpdateQuizCommandValidator()
    {
        RuleFor(p => p.StepId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}