using LMS.Domain.Common;

namespace LMS.Domain.Entities;

public class Step: AuditableEntity
{
    public Guid Id { get; private set; }
    public Guid CourseId { get; private set; }
    
    private Step(Guid courseId)
    {
        Id = Guid.NewGuid();
        CourseId = courseId;
    }

    public static Result<Step> Create(Guid courseId)
    {
        if (courseId == default)
        {
            return Result<Step>.Failure("course id should not be default");
        }
        
        return Result<Step>.Success(new Step(courseId));
    }
    
    public static Result<Step> Update(Step step, Guid courseId)
    {
        if (courseId == default)
        {
            return Result<Step>.Failure("course id should not be default");
        }
        
        step.CourseId = courseId;
        
        return Result<Step>.Success(step);
    }
}