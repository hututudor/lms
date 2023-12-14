using System.ComponentModel.DataAnnotations;

namespace LMS.App.ViewModels;

public class CourseDto
{
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Name is required")]
    public Guid UserId { get; set; }
}