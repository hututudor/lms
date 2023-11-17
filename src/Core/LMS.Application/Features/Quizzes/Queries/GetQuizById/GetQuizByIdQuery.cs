using MediatR;

namespace LMS.Application.Features.Quizzes.Queries.GetById;

public record GetQuizByIdQuery(Guid Id) : IRequest<QuizDto>;