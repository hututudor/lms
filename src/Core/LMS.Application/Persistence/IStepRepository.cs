using LMS.Domain.Common;
using LMS.Domain.Entities;

namespace LMS.Application.Persistence;

public interface IStepRepository: IAsyncRepository<Step>
{
    Task<Result<IReadOnlyList<Step>>> GetAllByCourseId(Guid stepId);
}