using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Lectures.Queries.GetAll;

public class GetAllLecturesQueryHandler: IRequestHandler<GetAllLecturesQuery, GetAllLecturesQueryResponse>
{
    private readonly ILectureRepository repository;

    public GetAllLecturesQueryHandler(ILectureRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<GetAllLecturesQueryResponse> Handle(GetAllLecturesQuery request, CancellationToken cancellationToken)
    {
        var lectures = await repository.GetAll();
        var lecturesDto = lectures.Value.Select(lecture => new LectureDto
        {
            Id = lecture.Id,
            StepId = lecture.StepId,
            Name = lecture.Name,
            Url = lecture.Url
        }).ToList();

        return new GetAllLecturesQueryResponse
        {
            Lectures = lecturesDto
        };
    }
}