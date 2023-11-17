using MediatR;

namespace LMS.Application.Features.Questions.Commands.CreateQuestion;

public class CreateQuestionCommand: IRequest<CreateQuestionCommandResponse>
{
    public Guid QuizId { get; set; }
    public string Description { get; set; }
    public string Answer { get; set; }
    public List<string> Options { get; set; }
}