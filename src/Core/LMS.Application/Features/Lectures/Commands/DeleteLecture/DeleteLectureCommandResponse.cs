using LMS.Application.Responses;

namespace LMS.Application.Features.Lectures.Commands.DeleteLecture;

public class DeleteLectureCommandResponse: BaseResponse
{
   public DeleteLectureCommandResponse() : base()
   {
       
   }
    
   public LectureDto Lecture { get; set; }
}