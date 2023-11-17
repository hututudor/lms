namespace LMS.Application.Features.Quizzes;

public class QuizDto
{
    public QuizDto()
    {
    }
    
    public Guid Id { get; set; }
    public Guid StepId { get; set; }
    public string Name { get; set; }
}