namespace LMS.Application.Features.Steps.Queries.GetAllStepsByCourseId;

public class GetAllStepsByCourseIdQueryResponse
{
    public GetAllStepsByCourseIdQueryResponse()
    {
    }
    
    public List<StepDto> Steps { get; set; }
}