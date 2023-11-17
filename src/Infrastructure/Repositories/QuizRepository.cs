using LMS.Application.Persistence;
using LMS.Domain.Common;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class QuizRepository: BaseRepository<Quiz>, IQuizRepository
{
    public QuizRepository(GlobalLMSContext context) : base(context)
    {
    }
    
    public virtual async Task<Result<IReadOnlyList<Quiz>>> GetAllByStepId(Guid stepId)
    {
        var result = await context.Set<Quiz>().AsNoTracking().Where(e => e.StepId == stepId).ToListAsync();
        return Result<IReadOnlyList<Quiz>>.Success(result);
    }
}