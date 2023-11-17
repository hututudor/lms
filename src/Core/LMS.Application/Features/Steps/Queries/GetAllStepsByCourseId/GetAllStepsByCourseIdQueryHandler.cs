using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Steps.Queries.GetAllStepsByCourseId;

public class GetAllStepsByCourseIdQueryHandler: IRequestHandler<GetAllStepsByCourseIdQuery, GetAllStepsByCourseIdQueryResponse>
{
    private readonly IStepRepository repository;

    public GetAllStepsByCourseIdQueryHandler(IStepRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<GetAllStepsByCourseIdQueryResponse> Handle(GetAllStepsByCourseIdQuery request, CancellationToken cancellationToken)
    {
        var steps = await repository.GetAllByCourseId(request.CourseId);
        var stepsDto = steps.Value.Select(enrollment => new StepDto
        {
            Id = enrollment.Id,
            CourseId = enrollment.CourseId,
        }).ToList();

        return new GetAllStepsByCourseIdQueryResponse
        {
            Steps = stepsDto
        };
    }

}