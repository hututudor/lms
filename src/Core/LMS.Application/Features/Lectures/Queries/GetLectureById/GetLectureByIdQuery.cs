using MediatR;

namespace LMS.Application.Features.Lectures.Queries.GetById;

public record GetLectureByIdQuery(Guid Id) : IRequest<LectureDto>;