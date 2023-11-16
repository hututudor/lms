using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Lectures.Queries.GetById;

public class GetLectureByIdHandler: IRequestHandler<GetLectureByIdQuery, LectureDto?>
{
    private readonly ILectureRepository repository;

    public GetLectureByIdHandler(ILectureRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<LectureDto?> Handle(GetLectureByIdQuery request, CancellationToken cancellationToken)
    {
        var lecture = await repository.GetByIdAsync(request.Id);
        return !lecture.IsSuccess ? null : new LectureDto
        {
            Id = lecture.Value.Id,
            StepId = lecture.Value.StepId,
            Name = lecture.Value.Name,
            Url = lecture.Value.Url
        };
    }   
}