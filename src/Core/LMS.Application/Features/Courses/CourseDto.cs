namespace LMS.Application.Features.Courses;

public class CourseDto
{
    public CourseDto()
    {
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }
}