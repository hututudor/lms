using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Steps.Queries.GetStepById;

public class GetStepByIdQueryHandler: IRequestHandler<GetStepByIdQuery, StepDto?>
{
    private readonly IStepRepository repository;

    public GetStepByIdQueryHandler(IStepRepository repository)
    {
        this.repository = repository;
    }

    public async Task<StepDto?> Handle(GetStepByIdQuery request, CancellationToken cancellationToken)
    {
        var lecture = await repository.GetByIdAsync(request.Id);
        return !lecture.IsSuccess ? null : new StepDto
        {
            Id = lecture.Value.Id,
            CourseId = lecture.Value.CourseId,
        };
    }  
}