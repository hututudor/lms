using LMS.Domain.Entities;

namespace LMS.Application.Persistence;

public interface ILectureRepository: IAsyncRepository<Lecture>
{
}