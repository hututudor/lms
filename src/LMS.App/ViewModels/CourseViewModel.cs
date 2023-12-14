using System.ComponentModel.DataAnnotations;

namespace LMS.App.ViewModels
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}