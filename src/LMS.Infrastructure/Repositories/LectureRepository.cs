using LMS.Application.Persistence;
using LMS.Domain.Common;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LectureRepository: BaseRepository<Lecture>, ILectureRepository
{
    public LectureRepository(GlobalLMSContext context) : base(context)
    {
    }
    
    public virtual async Task<Result<IReadOnlyList<Lecture>>> GetAllByStepId(Guid stepId)
    {
        var result = await context.Set<Lecture>().AsNoTracking().Where(e => e.StepId == stepId).ToListAsync();
        return Result<IReadOnlyList<Lecture>>.Success(result);
    }
}