using FluentAssertions;
using LMS.Domain.Entities;

namespace LMS.Core.Domain.Tests;

public class QuizProgressTests
{
    [Fact]
    public void When_CreateQuizProgressIsCalled_And_QuizIdIsValid_And_EnrollmentIdIsValid_And_ScoreIsValid_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = QuizProgress.Create(Guid.NewGuid(), Guid.NewGuid(), 1);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_CreateQuizProgressIsCalled_And_QuizIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = QuizProgress.Create(Guid.Empty, Guid.NewGuid(), 1);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateQuizProgressIsCalled_And_EnrollmentIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = QuizProgress.Create(Guid.NewGuid(), Guid.Empty, 1);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateQuizProgressIsCalled_And_ScoreIsLessThanZero_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = QuizProgress.Create(Guid.NewGuid(), Guid.NewGuid(), -1);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}