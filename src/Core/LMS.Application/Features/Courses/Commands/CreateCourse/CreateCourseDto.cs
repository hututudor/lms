namespace LMS.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseDto
    {
        public Guid CourseId { get; set; }
        public string? CourseName { get; set; }
        public Guid UserId { get; set; }
    }
}