using LMS.Application.Persistence;
using LMS.Domain.Common;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EnrollmentRepository: BaseRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(GlobalLMSContext context): base(context)
    {
    }
    
    public virtual async Task<Result<IReadOnlyList<Enrollment>>> GetAllByUserId(Guid userId)
    {
        var result = await context.Set<Enrollment>().AsNoTracking().Where(e => e.UserId == userId).ToListAsync();
        return Result<IReadOnlyList<Enrollment>>.Success(result);
    }
}