using LMS.Domain.Common;
using LMS.Domain.Entities;

namespace LMS.Application.Persistence;

public interface ILectureRepository: IAsyncRepository<Lecture>
{
    Task<Result<IReadOnlyList<Lecture>>> GetAllByStepId(Guid stepId);
}