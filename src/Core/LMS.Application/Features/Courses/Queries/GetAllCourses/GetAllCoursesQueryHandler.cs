using CourseRepository;
using LMS.Application.Features.Courses;
using LMS.Application.Features.Courses.Queries;
using MediatR;


public class GetAllCoursesQueryHandler: IRequestHandler<GetAllCoursesQuery, GetAllCoursesQueryResponse>
{
    private readonly ICourseRepository repository;
    
    public GetAllCoursesQueryHandler(ICourseRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<GetAllCoursesQueryResponse> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await repository.GetAll();
        var coursesDto = courses.Value.Select(course => new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            UserId = course.UserId
        }).ToList();

        return new GetAllCoursesQueryResponse
        {
            Courses = coursesDto
        };
    }
}