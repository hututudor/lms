using CourseRepository;
using LMS.Application.Features.Courses.Queries.GetCourseById;
using MediatR;

namespace LMS.Application.Features.Courses.Queries.GetById;

public class GetCourseByIdHandler: IRequestHandler<GetCourseByIdQuery, CourseDto>
{
    private readonly ICourseRepository repository;

    public GetCourseByIdHandler(ICourseRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await repository.GetByIdAsync(request.Id);
        return !course.IsSuccess ? null : new CourseDto
        {
            Id = course.Value.Id,
            Name = course.Value.Name,
            UserId = course.Value.UserId
        };
    }

}