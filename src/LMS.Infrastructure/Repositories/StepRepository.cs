using LMS.Application.Persistence;
using LMS.Domain.Common;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StepRepository: BaseRepository<Step>, IStepRepository
{
    public StepRepository(GlobalLMSContext context): base(context)
    {
    }
    
    public virtual async Task<Result<IReadOnlyList<Step>>> GetAllByCourseId(Guid courseId)
    {
        var result = await context.Set<Step>().AsNoTracking().Where(e => e.CourseId == courseId).ToListAsync();
        return Result<IReadOnlyList<Step>>.Success(result);
    }
}