using LMS.Application.Persistence;
using LMS.Domain.Entities;

namespace CourseRepository
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
         
    }
}