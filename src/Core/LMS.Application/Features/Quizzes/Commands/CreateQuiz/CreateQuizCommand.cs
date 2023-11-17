using MediatR;

namespace LMS.Application.Features.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommand: IRequest<CreateQuizCommandResponse>
{
    public Guid StepId { get; set; }
    public string Name { get; set; }
}