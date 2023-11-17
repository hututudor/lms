using LMS.Domain.Common;
using LMS.Domain.Entities;

namespace LMS.Application.Persistence;

public interface IQuestionRepository: IAsyncRepository<Question>
{
    Task<Result<IReadOnlyList<Question>>> GetAllByQuizId(Guid quizId);
}