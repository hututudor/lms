namespace LMS.Domain.Entities;
using LMS.Domain.Common;

public class StepProgress
{
    public Guid Id { get; private set; }
    public Guid EnrollmentId { get; private set; }
    public Guid StepId { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private StepProgress(Guid enrollmentId, Guid stepId)
    {
        Id = Guid.NewGuid();
        EnrollmentId = enrollmentId;
        StepId = stepId;
    }

    public static Result<StepProgress> Create(Guid enrollmentId, Guid stepId)
    {
        if (enrollmentId == default)
        {
            return Result<StepProgress>.Failure("enrollmentId is required");
        }

        if (stepId == default)
        {
            return Result<StepProgress>.Failure("stepId is required");
        }

        return Result<StepProgress>.Success(new StepProgress(enrollmentId, stepId));
    }
    
    public void AttachEnrollment(Guid enrollmentId)
    {
        if (enrollmentId == default)
        {
            return;
        }

        EnrollmentId = enrollmentId;
    }
    
    public void AttachStep(Guid stepId)
    {
        if (stepId == default)
        {
            return;
        }

        StepId = stepId;
    }
    
    public void Complete()
    {
        CompletedAt = DateTime.Now;
    }
}