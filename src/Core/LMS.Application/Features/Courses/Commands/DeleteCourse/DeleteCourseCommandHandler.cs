using CourseRepository;
using MediatR;

namespace LMS.Application.Features.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler: IRequestHandler<DeleteCourseCommand, DeleteCourseCommandResponse>
{
    private readonly ICourseRepository repository;

    public DeleteCourseCommandHandler(ICourseRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DeleteCourseCommandResponse> Handle(DeleteCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = await repository.GetByIdAsync(request.Id);
        if (!course.IsSuccess)
        {
            return new DeleteCourseCommandResponse
            {
                Message = $"Course with id {request.Id} not found",
                Success = false
            };
        }

        await repository.DeleteAsync(request.Id);
        return new DeleteCourseCommandResponse
        {
            Success = true
        };
    }
}