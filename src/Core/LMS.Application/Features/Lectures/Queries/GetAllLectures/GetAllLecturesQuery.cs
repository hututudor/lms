using MediatR;

namespace LMS.Application.Features.Lectures.Queries.GetAll;

public record GetAllLecturesQuery(Guid StepId) : IRequest<GetAllLecturesQueryResponse>;