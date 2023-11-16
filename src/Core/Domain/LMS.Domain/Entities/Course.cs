using LMS.Domain.Common;

namespace LMS.Domain.Entities;

public class Course: AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid UserId { get; private set; }
    
    private Course(string name, Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        UserId = userId;
    }

    public static Result<Course> Create(string name, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Course>.Failure("name is required");
        }        
        
        if (userId == default)
        {
            return Result<Course>.Failure("user id should not be default");
        }
        
        return Result<Course>.Success(new Course(name, userId));
    }
    
    public static Result<Course> Update(Course course, string name, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Course>.Failure("name is required");
        }

        if (userId == default)
        {
            return Result<Course>.Failure("user id should not be default");
        }

        course.UserId = userId;
        course.Name = name;
        
        return Result<Course>.Success(course);
    }
    
    public void AttachUser(Guid userId)
    {
        if (userId == default)
        {
            return;
        }

        UserId = userId;
    }
}