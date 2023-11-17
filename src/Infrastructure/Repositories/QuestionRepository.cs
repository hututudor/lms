using LMS.Application.Persistence;
using LMS.Domain.Common;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class QuestionRepository: BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(GlobalLMSContext context) : base(context)
    {
    }
    
    public virtual async Task<Result<IReadOnlyList<Question>>> GetAllByQuizId(Guid quizId)
    {
        var result = await context.Set<Question>().AsNoTracking().Where(e => e.QuizId == quizId).ToListAsync();
        return Result<IReadOnlyList<Question>>.Success(result);
    }
}