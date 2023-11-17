using MediatR;

namespace LMS.Application.Features.Quizzes.Queries.GetAll;

public record GetAllQuizzesQuery(Guid StepId) : IRequest<GetAllQuizzesQueryResponse>;