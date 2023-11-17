namespace LMS.Application.Features.Enrollments;

public class EnrollmentDto
{
    public EnrollmentDto()
    {
    }
    
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
    public DateTime? CompletedAt { get; set; }
}