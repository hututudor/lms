using CourseRepository;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler: IRequestHandler<UpdateCourseCommand, UpdateCourseCommandResponse>
{
    private readonly ICourseRepository repository;

    public UpdateCourseCommandHandler(ICourseRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UpdateCourseCommandResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.GetByIdAsync(request.Id);
        if (!course.IsSuccess)
        {
            return new UpdateCourseCommandResponse
            {
                Message = $"Course with id {request.Id} not found",
                Success = false
            };
        }
        
        var updatedCourse = Course.Update(course.Value, request.Name, request.UserId);
        if (!course.IsSuccess)
        {
            return new UpdateCourseCommandResponse
            {
                Message = updatedCourse.Error,
                Success = false
            };
        }

        await repository.UpdateAsync(updatedCourse.Value);
        
        return new UpdateCourseCommandResponse
        {
            Success = true,
            Course = new CourseDto
            {
                Id = course.Value.Id,
                Name = updatedCourse.Value.Name,
                UserId = updatedCourse.Value.UserId
            }
        };
    }
}