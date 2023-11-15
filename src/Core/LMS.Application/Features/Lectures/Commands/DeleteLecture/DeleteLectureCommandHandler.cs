using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Lectures.Commands.DeleteLecture;

public class DeleteLectureCommandHandler: IRequestHandler<DeleteLectureCommand, DeleteLectureCommandResponse>
{
    private readonly ILectureRepository repository;

    public DeleteLectureCommandHandler(ILectureRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DeleteLectureCommandResponse> Handle(DeleteLectureCommand request,
        CancellationToken cancellationToken)
    {
        var lecture = await repository.GetByIdAsync(request.Id);
        if (!lecture.IsSuccess)
        {
            return new DeleteLectureCommandResponse
            {
                Message = $"Lecture with id {request.Id} not found",
                Success = false
            };
        }

        await repository.DeleteAsync(request.Id);
        return new DeleteLectureCommandResponse
        {
            Success = true
        };
    }
}