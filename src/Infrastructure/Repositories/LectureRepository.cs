using LMS.Application.Persistence;
using LMS.Domain.Entities;

namespace Infrastructure.Repositories;

public class LectureRepository: BaseRepository<Lecture>, ILectureRepository
{
    public LectureRepository(GlobalLMSContext context) : base(context)
    {
    }
}