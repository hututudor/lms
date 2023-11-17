using LMS.Domain.Common;
using LMS.Domain.Entities;

namespace LMS.Application.Persistence;

public interface IQuizRepository: IAsyncRepository<Quiz>
{
    Task<Result<IReadOnlyList<Quiz>>> GetAllByStepId(Guid stepId);

}