using MediatR;

namespace LMS.Application.Features.Questions.Queries.GetById;

public record GetQuestionByIdQuery(Guid Id) : IRequest<QuestionDto>;