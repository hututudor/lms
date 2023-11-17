using MediatR;

namespace LMS.Application.Features.Questions.Commands.DeleteQuestion;

public record DeleteQuestionCommand(Guid Id): IRequest<DeleteQuestionCommandResponse>;