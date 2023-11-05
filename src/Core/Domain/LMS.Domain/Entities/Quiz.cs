namespace LMS.Domain.Entities;
using LMS.Domain.Common;

public class Quiz
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid StepId { get; private set; }
    
    private Quiz(string name, Guid stepId)
    {
        Id = Guid.NewGuid();
        Name = name;
        StepId = stepId;
    }

    public static Result<Quiz> Create(string name, Guid stepId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Quiz>.Failure("name is required");
        }        
        
        if (stepId == default)
        {
            return Result<Quiz>.Failure("step id should not be default");
        }
        
        return Result<Quiz>.Success(new Quiz(name, stepId));
    }
    
    public void AttachStep(Guid stepId)
    {
        if (stepId == default)
        {
            return;
        }

        StepId = stepId;
    }
}