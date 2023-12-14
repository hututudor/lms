using System.ComponentModel.DataAnnotations;

namespace LMS.App.ViewModels;

public class CourseDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}