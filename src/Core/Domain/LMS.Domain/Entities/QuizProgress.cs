namespace LMS.Domain.Entities;
using LMS.Domain.Common;


public class QuizProgress
{
    public Guid Id { get; private set; }
    public Guid QuizId { get; private set; }
    public Guid EnrollmentId { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public int Score { get; private set; }

    private QuizProgress(Guid quizId, Guid enrollmentId, int score)
    {
        Id = Guid.NewGuid();
        QuizId = quizId;
        EnrollmentId = enrollmentId;
        Score = score;
    }

    public static Result<QuizProgress> Create(Guid quizId, Guid enrollmentId, int score)
    {
        if (quizId == default)
        {
            return Result<QuizProgress>.Failure("quizId is required");
        }

        if (enrollmentId == default)
        {
            return Result<QuizProgress>.Failure("enrollmentId is required");
        }
        
        if (score < 0)
        {
            return Result<QuizProgress>.Failure("score should not be less than zero");
        }

        return Result<QuizProgress>.Success(new QuizProgress(quizId, enrollmentId, score));
    }
    
    public void AttachEnrollment(Guid enrollmentId)
    {
        if (enrollmentId == default)
        {
            return;
        }

        EnrollmentId = enrollmentId;
    }
    
    public void AttachQuiz(Guid quizId)
    {
        if (quizId == default)
        {
            return;
        }

        QuizId = quizId;
    }
    
    public void Complete()
    {
        CompletedAt = DateTime.Now;
    }
}