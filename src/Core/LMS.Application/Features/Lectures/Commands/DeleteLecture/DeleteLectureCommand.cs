using MediatR;

namespace LMS.Application.Features.Lectures.Commands.DeleteLecture;

public record DeleteLectureCommand(Guid Id): IRequest<DeleteLectureCommandResponse>;