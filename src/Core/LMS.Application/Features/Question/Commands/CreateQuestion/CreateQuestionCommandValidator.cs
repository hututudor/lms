using FluentValidation;

namespace LMS.Application.Features.Questions.Commands.CreateQuestion;

public class CreateQuestionCommandValidator: AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
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