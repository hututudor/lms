using LMS.Domain.Common;
using LMS.Domain.Entities;

namespace LMS.Application.Persistence;

public interface IEnrollmentRepository: IAsyncRepository<Enrollment>
{
    Task<Result<IReadOnlyList<Enrollment>>> GetAllByUserId(Guid userId);
}