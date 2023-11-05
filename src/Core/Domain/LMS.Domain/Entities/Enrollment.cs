using LMS.Domain.Common;

namespace LMS.Domain.Entities;

public class Enrollment: AuditableEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    
    private Enrollment(Guid userId, Guid courseId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CourseId = courseId;
    }

    public static Result<Enrollment> Create(Guid userId, Guid courseId)
    {
        if (userId == default)
        {
            return Result<Enrollment>.Failure("user id should not be default");
        }        
        
        if (courseId == default)
        {
            return Result<Enrollment>.Failure("course id should not be default");
        }
        
        return Result<Enrollment>.Success(new Enrollment(userId, courseId));
    }

    public void AttachUser(Guid userId)
    {
        if (userId == default)
        {
            return;
        }

        UserId = userId;
    }

    public void AttachCourse(Guid courseId)
    {
        if (courseId == default)
        {
            return;
        }

        CourseId = courseId;
    }

    public void Complete()
    {
        CompletedAt = DateTime.Now;
    }
}