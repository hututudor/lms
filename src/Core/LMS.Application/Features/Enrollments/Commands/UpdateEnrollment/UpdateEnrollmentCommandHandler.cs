using LMS.Application.Persistence;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.Enrollments.Commands.UpdateEnrollment;

public class UpdateEnrollmentCommandHandler: IRequestHandler<UpdateEnrollmentCommand, UpdateEnrollmentCommandResponse>
{
    private readonly IEnrollmentRepository repository;

    public UpdateEnrollmentCommandHandler(IEnrollmentRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UpdateEnrollmentCommandResponse> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await repository.GetByIdAsync(request.Id);
        if (!enrollment.IsSuccess)
        {
            return new UpdateEnrollmentCommandResponse
            {
                Message = $"Lecture with id {request.Id} not found",
                Success = false
            };
        }
        
        var updatedEnrollment = Enrollment.Update(enrollment.Value, request.UserId, request.CourseId, request.CompletedAt);
        if (!updatedEnrollment.IsSuccess)
        {
            return new UpdateEnrollmentCommandResponse
            {
                Message = updatedEnrollment.Error,
                Success = false
            };
        }

        await repository.UpdateAsync(updatedEnrollment.Value);
        
        return new UpdateEnrollmentCommandResponse
        {
            Success = true,
            Enrollment = new EnrollmentDto
            {
                Id = enrollment.Value.Id,
                UserId = updatedEnrollment.Value.UserId,
                CourseId = updatedEnrollment.Value.CourseId,
                CompletedAt = updatedEnrollment.Value.CompletedAt
            }
        };
    }

}