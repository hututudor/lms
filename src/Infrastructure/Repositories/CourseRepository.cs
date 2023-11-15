using CourseRepository;
using LMS.Domain.Entities;

namespace Infrastructure.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(GlobalLMSContext context) : base(context)
        {
        }
    }
}