using System.ComponentModel.DataAnnotations;

namespace LMS.App.ViewModels
{
    public class StepViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "CourseId is required")]
        public Guid CourseId { get; set; }
    }
}