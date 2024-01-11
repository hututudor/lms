namespace LMS.Domain.Entities;
using LMS.Domain.Common;

public class QuizResponse
{
    public Guid Id { get; private set; }
    public Guid QuestionId { get; private set; }
    public Guid EnrollmentId { get; private set; }
    public string Response { get; private set; }

    private QuizResponse(Guid questionId, Guid enrollmentId, string response)
    {
        Id = Guid.NewGuid();
        QuestionId = questionId;
        EnrollmentId = enrollmentId;
        Response = response;
    }

    public static Result<QuizResponse> Create(Guid questionId, Guid enrollmentId, string response)
    {
        if (questionId == default)
        {
            return Result<QuizResponse>.Failure("questionId is required");
        }

        if (enrollmentId == default)
        {
            return Result<QuizResponse>.Failure("enrollmentId is required");
        }

        if (string.IsNullOrWhiteSpace(response))
        {
            return Result<QuizResponse>.Failure("response is required");
        }

        return Result<QuizResponse>.Success(new QuizResponse(questionId, enrollmentId, response));
    }
}
