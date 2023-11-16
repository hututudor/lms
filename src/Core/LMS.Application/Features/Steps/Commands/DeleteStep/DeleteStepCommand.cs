using MediatR;

namespace LMS.Application.Features.Steps.Commands.DeleteStep;

public record DeleteStepCommand(Guid Id): IRequest<DeleteStepCommandResponse>;