using LMS.Application.Persistence;
using MediatR;

namespace LMS.Application.Features.Quizzes.Queries.GetAll;

public class GetAllQuizzesQueryHandler: IRequestHandler<GetAllQuizzesQuery, GetAllQuizzesQueryResponse>
{
    private readonly IQuizRepository repository;

    public GetAllQuizzesQueryHandler(IQuizRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<GetAllQuizzesQueryResponse> Handle(GetAllQuizzesQuery request, CancellationToken cancellationToken)
    {
        var quizzes = await repository.GetAllByStepId(request.StepId);
        var quizzesDto = quizzes.Value.Select(lecture => new QuizDto()
        {
            Id = lecture.Id,
            StepId = lecture.StepId,
            Name = lecture.Name,
        }).ToList();

        return new GetAllQuizzesQueryResponse
        {
            Quizzes = quizzesDto
        };
    }
}