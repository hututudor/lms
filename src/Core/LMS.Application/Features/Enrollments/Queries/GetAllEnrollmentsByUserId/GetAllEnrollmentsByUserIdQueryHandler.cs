using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Enrollments.Queries.GetAllEnrollmentsByUserId;

public class GetAllEnrollmentsByUserIdQueryHandler: IRequestHandler<GetAllEnrollmentsByUserIdQuery, GetAllEnrollmentsByUserIdQueryResponse>
{    
    private readonly IEnrollmentRepository repository;

    public GetAllEnrollmentsByUserIdQueryHandler(IEnrollmentRepository repository)
    {
        this.repository = repository;
    }

    public async Task<GetAllEnrollmentsByUserIdQueryResponse> Handle(GetAllEnrollmentsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await repository.GetAllByUserId(request.UserId);
        var enrollmentsDto = enrollments.Value.Select(enrollment => new EnrollmentDto
        {
            Id = enrollment.Id,
            CourseId = enrollment.CourseId,
            UserId = enrollment.UserId,
            CompletedAt = enrollment.CompletedAt
        }).ToList();

        return new GetAllEnrollmentsByUserIdQueryResponse
        {
            Enrollments = enrollmentsDto
        };
    }
}