using System.ComponentModel.DataAnnotations;

namespace LMS.App.ViewModels;

public class StepDto
{
    public StepDto()
    {
    }
    
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}