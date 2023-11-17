using LMS.Application.Features.Enrollments.Queries.GetAllEnrollmentsByUserId;
using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Enrollments.Queries.GetEnrollmentById;

public class GetEnrollmentByIdQueryHandler: IRequestHandler<GetEnrollmentByIdQuery, EnrollmentDto?>
{
    private readonly IEnrollmentRepository repository;

    public GetEnrollmentByIdQueryHandler(IEnrollmentRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<EnrollmentDto?> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
    {
        var lecture = await repository.GetByIdAsync(request.Id);
        return !lecture.IsSuccess ? null : new EnrollmentDto
        {
            Id = lecture.Value.Id,
            CourseId = lecture.Value.CourseId,
            UserId = lecture.Value.UserId,
            CompletedAt = lecture.Value.CompletedAt
        };
    }   

}