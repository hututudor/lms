using MediatR;

namespace LMS.Application.Features.Questions.Queries.GetAll;

public record GetAllQuestionsQuery(Guid QuizId) : IRequest<GetAllQuestionsQueryResponse>;