namespace LMS.Application.Features.Lectures;

public class LectureDto
{
    public LectureDto()
    {
    }
    
    public Guid Id { get; set; }
    public Guid StepId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}