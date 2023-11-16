using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentCommandHandler: IRequestHandler<CreateEnrollmentCommand, CreateEnrollmentCommandResponse>
{
    private readonly IEnrollmentRepository repository;

    public CreateEnrollmentCommandHandler(IEnrollmentRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<CreateEnrollmentCommandResponse> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateEnrollmentCommandValidator();
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatorResult.IsValid)
        {
            return new CreateEnrollmentCommandResponse
            {
                Success = false,
                ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var enrollment = Enrollment.Create(request.UserId, request.CourseId);
        if (!enrollment.IsSuccess)
        {
            return new CreateEnrollmentCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { enrollment.Error }
            };
        }

        await repository.AddAsync(enrollment.Value);

        return new CreateEnrollmentCommandResponse
        {
            Success = true,
            Enrollment = new EnrollmentDto
            {
                Id = enrollment.Value.Id,
                UserId = enrollment.Value.UserId,
                CourseId = enrollment.Value.CourseId,
                CompletedAt = enrollment.Value.CompletedAt
            }
        };
    }

}