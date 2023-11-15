using LMS.Application.Responses;

namespace LMS.Application.Features.Lectures.Queries.GetAll;

public class GetAllLecturesQueryResponse: BaseResponse
{
    public List<LectureDto> Lectures { get; set; }
}