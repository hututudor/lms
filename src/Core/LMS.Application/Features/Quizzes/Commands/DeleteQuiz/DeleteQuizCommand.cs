using MediatR;

namespace LMS.Application.Features.Quizzes.Commands.DeleteQuiz;

public record DeleteQuizCommand(Guid Id): IRequest<DeleteQuizCommandResponse>;