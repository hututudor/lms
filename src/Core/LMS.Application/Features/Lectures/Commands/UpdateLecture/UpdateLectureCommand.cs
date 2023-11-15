using MediatR;

namespace LMS.Application.Features.Lectures.Commands.UpdateLecture;

public class UpdateLectureCommand: IRequest<UpdateLectureCommandResponse>
{
    public Guid Id { get; set; }
    public Guid StepId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}