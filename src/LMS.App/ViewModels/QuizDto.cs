using System.ComponentModel.DataAnnotations;

namespace LMS.App.ViewModels;

public class QuizDto
{
    public QuizDto()
    {
    }
    
    public Guid StepId { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
}