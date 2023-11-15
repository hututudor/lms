using MediatR;

namespace LMS.Application.Features.Lectures.Queries.GetAll;

public record GetAllLecturesQuery(Guid Id) : IRequest<GetAllLecturesQueryResponse>;