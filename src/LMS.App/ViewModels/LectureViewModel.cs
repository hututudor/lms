using System.ComponentModel.DataAnnotations;

namespace LMS.App.ViewModels
{
    public class LectureViewModel
    {
        public Guid Id { get; set; }
        public Guid StepId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; } = string.Empty;
    }
}