using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Lectures.Commands.CreateLecture;

public class CreateLectureCommandHandler: IRequestHandler<CreateLectureCommand, CreateLectureCommandResponse>
{
    private readonly ILectureRepository repository;

    public CreateLectureCommandHandler(ILectureRepository repository)
    {
        this.repository = repository;
    }

    public async Task<CreateLectureCommandResponse> Handle(CreateLectureCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLectureCommandValidator();
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
        {
            return new CreateLectureCommandResponse()
            {
                Success = false,
                ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }
        
        var course = Lecture.Create(request.StepId, request.Name, request.Url);
        if (!course.IsSuccess)
        {
            return new CreateLectureCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { course.Error }
            };
        }

        await repository.AddAsync(course.Value);

        return new CreateLectureCommandResponse
        {
            Success = true,
            Lecture = new LectureDto
            {
                Id = course.Value.Id,
                StepId = course.Value.StepId,
                Name = course.Value.Name,
                Url = course.Value.Url
            }
        };
    }
}