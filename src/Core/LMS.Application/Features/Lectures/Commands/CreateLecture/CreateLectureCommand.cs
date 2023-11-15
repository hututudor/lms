using MediatR;

namespace LMS.Application.Features.Lectures.Commands.CreateLecture;

public class CreateLectureCommand: IRequest<CreateLectureCommandResponse>
{
    public Guid StepId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}