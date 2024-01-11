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
    
    public static Result<Enrollment> Update(Enrollment enrollment, Guid userId, Guid courseId, DateTime? completedAt)
    {
        if (userId == default)
        {
            return Result<Enrollment>.Failure("user id should not be default");
        }        
        
        if (courseId == default)
        {
            return Result<Enrollment>.Failure("course id should not be default");
        }

        enrollment.UserId = userId;
        enrollment.CourseId = courseId;
        enrollment.CompletedAt = completedAt;
        
        return Result<Enrollment>.Success(enrollment);
    }
}