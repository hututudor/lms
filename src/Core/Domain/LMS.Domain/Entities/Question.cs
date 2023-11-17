using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace LMS.Domain.Entities;
using LMS.Domain.Common;

public class Question
{
    public Guid Id { get; private set; }
    public Guid QuizId { get; private set; }
    public string Description { get; private set; }
    public string Answer { get; private set; }
    public List<string> Options { get; private set; }

    private Question(Guid quizId, string description, string answer, List<string> options)
    {
        Id = Guid.NewGuid();
        QuizId = quizId;
        Description = description;
        Answer = answer;
        Options = options;
    }
    
    public static Result<Question> Update(Question question, Guid quizId, string description, string answear, List<string> options)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return Result<Question>.Failure("description is required");
        }

        if (quizId == default)
        {
            return Result<Question>.Failure("quiz id should not be default");
        }

        question.Description = description;
        question.Options = options;
        question.Answer = answear;
        
        return Result<Question>.Success(question);
    }
    
    public static Result<Question> Create(Guid quizId, string description, string answer, List<string> options)
    {
        if (quizId == default)
        {
            return Result<Question>.Failure("quizId is required");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result<Question>.Failure("description is required");
        }

        if (string.IsNullOrWhiteSpace(answer))
        {
            return Result<Question>.Failure("answer is required");
        }

        return Result<Question>.Success(new Question(quizId, description, answer, options));
    }
    
    public void AttachQuiz(Guid quizId)
    {
        if (quizId == default)
        {
            return;
        }

        QuizId = quizId;
    }
}