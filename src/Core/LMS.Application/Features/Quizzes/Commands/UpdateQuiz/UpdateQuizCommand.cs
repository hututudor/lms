using LMS.Application.Features.Quizzes.Commands.UpdateQuiz;
using MediatR;

namespace LMS.Application.Features.Quizzes.Commands.UpdateQuiz;

public class UpdateQuizCommand: IRequest<UpdateQuizCommandResponse>
{
    public Guid Id { get; set; }
    public Guid StepId { get; set; }
    public string Name { get; set; }
}