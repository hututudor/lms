using FluentValidation;

namespace LMS.Application.Features.Questions.Commands.UpdateQuestion;

public class UpdateQuestionCommandValidator: AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(p => p.QuizId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        RuleFor(p => p.Answer)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}