namespace LMS.Application.Features.Enrollments.Queries.GetAllEnrollmentsByUserId;

public class GetAllEnrollmentsByUserIdQueryResponse
{
    public GetAllEnrollmentsByUserIdQueryResponse()
    {
        
    }   
    
    public List<EnrollmentDto> Enrollments { get; set; }
}