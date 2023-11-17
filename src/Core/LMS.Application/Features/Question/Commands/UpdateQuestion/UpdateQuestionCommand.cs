using MediatR;

namespace LMS.Application.Features.Questions.Commands.UpdateQuestion;

public class UpdateQuestionCommand: IRequest<UpdateQuestionCommandResponse>
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string Description { get; set; }
    public string Answer { get; set; }
    public List<string> Options { get; set; }
}