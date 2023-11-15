using LMS.Domain.Common;

namespace LMS.Domain.Entities;

public class Lecture: AuditableEntity
{
    public Guid Id { get; private set; }
    public Guid StepId { get; private set; }
    public string Name { get; private set; }
    public string Url { get; private set; }
    
    private Lecture(Guid stepId, string name, string url)
    {
        Id = Guid.NewGuid();
        StepId = stepId;
        Name = name;
        Url = url;
    }

    public static Result<Lecture> Create(Guid stepId, string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Lecture>.Failure("name is required");
        }
        
        if (string.IsNullOrWhiteSpace(url))
        {
            return Result<Lecture>.Failure("url is required");
        }
        
        if (stepId == default)
        {
            return Result<Lecture>.Failure("step id should not be default");
        }
        
        return Result<Lecture>.Success(new Lecture(stepId, name, url));
    }

    public static Result<Lecture> Update(Lecture lecture, Guid stepId, string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Lecture>.Failure("name is required");
        }

        if (string.IsNullOrWhiteSpace(url))
        {
            return Result<Lecture>.Failure("url is required");
        }

        if (stepId == default)
        {
            return Result<Lecture>.Failure("step id should not be default");
        }

        lecture.StepId = stepId;
        lecture.Name = name;
        lecture.Url = url;
        
        return Result<Lecture>.Success(lecture);
    }

}