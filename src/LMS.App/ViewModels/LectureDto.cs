namespace LMS.App.ViewModels;

public class LectureDto
{
    public LectureDto()
    {
    }
    public Guid Id { get; set; }
    public Guid StepId { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}