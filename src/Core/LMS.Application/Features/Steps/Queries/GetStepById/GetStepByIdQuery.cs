using MediatR;

namespace LMS.Application.Features.Steps.Queries.GetStepById;

public record GetStepByIdQuery(Guid Id): IRequest<StepDto?>;