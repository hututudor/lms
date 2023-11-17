namespace LMS.Application.Features.Questions;

public class QuestionDto
{
    public QuestionDto()
    {
    }
    
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string Description { get; set; }
    public string Answer { get; set; }
    public List<string> Options { get; set; }
}