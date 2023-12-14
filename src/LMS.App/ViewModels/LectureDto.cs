using System.ComponentModel.DataAnnotations;

namespace LMS.App.ViewModels;

public class LectureDto
{
    public Guid Id { get; set; }
    public Guid StepId { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Url is required")]
    public string Url { get; set; }
}