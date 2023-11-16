using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Enrollments.Commands.DeleteEnrollment;

public class DeleteEnrollmentCommandHandler: IRequestHandler<DeleteEnrollmentCommand, DeleteEnrollmentCommandResponse>
{
    private readonly IEnrollmentRepository repository;

    public DeleteEnrollmentCommandHandler(IEnrollmentRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<DeleteEnrollmentCommandResponse> Handle(DeleteEnrollmentCommand request,
        CancellationToken cancellationToken)
    {
        var enrollment = await repository.GetByIdAsync(request.Id);
        if (!enrollment.IsSuccess)
        {
            return new DeleteEnrollmentCommandResponse
            {
                Message = $"Enrollment with id {request.Id} not found",
                Success = false
            };
        }

        await repository.DeleteAsync(request.Id);
        return new DeleteEnrollmentCommandResponse
        {
            Success = true
        };
    }
}