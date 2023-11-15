using LMS.Application.Responses;

namespace LMS.Application.Features.Lectures.Commands.CreateLecture;

public class CreateLectureCommandResponse: BaseResponse
{
    public CreateLectureCommandResponse(): base()
    {
    }
    
    public LectureDto Lecture { get; set; }
}