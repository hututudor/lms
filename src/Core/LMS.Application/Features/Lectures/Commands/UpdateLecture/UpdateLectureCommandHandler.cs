using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Lectures.Commands.UpdateLecture;

public class UpdateLectureCommandHandler: IRequestHandler<UpdateLectureCommand, UpdateLectureCommandResponse>
{
    private readonly ILectureRepository repository;

    public UpdateLectureCommandHandler(ILectureRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UpdateLectureCommandResponse> Handle(UpdateLectureCommand request, CancellationToken cancellationToken)
    {
        var lecture = await repository.GetByIdAsync(request.Id);
        if (!lecture.IsSuccess)
        {
            return new UpdateLectureCommandResponse
            {
                Message = $"Lecture with id {request.Id} not found",
                Success = false
            };
        }
        
        var updatedLecture = Lecture.Update(lecture.Value, request.StepId, request.Name, request.Url);
        if (!updatedLecture.IsSuccess)
        {
            return new UpdateLectureCommandResponse
            {
                Message = updatedLecture.Error,
                Success = false
            };
        }

        await repository.UpdateAsync(updatedLecture.Value);
        
        return new UpdateLectureCommandResponse
        {
            Success = true,
            Lecture = new LectureDto
            {
                Id = lecture.Value.Id,
                StepId = updatedLecture.Value.StepId,
                Name = updatedLecture.Value.Name,
                Url = updatedLecture.Value.Url
            }
        };
    }
}