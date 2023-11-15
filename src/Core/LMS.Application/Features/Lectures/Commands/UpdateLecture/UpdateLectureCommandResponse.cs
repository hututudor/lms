using LMS.Application.Responses;

namespace LMS.Application.Features.Lectures.Commands.UpdateLecture;

public class UpdateLectureCommandResponse: BaseResponse
{
    public UpdateLectureCommandResponse(): base()
    {
    }
    
    public LectureDto Lecture { get; set; }
}