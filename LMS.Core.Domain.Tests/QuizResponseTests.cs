using FluentAssertions;
using LMS.Domain.Entities;

namespace LMS.Core.Domain.Tests;

public class QuizResponseTests
{
    [Fact]
    public void
        When_CreateQuizResponseIsCalled_And_QuestionIdIsValid_And_EnrollmentIdIsValid_And_ResponseIsValid_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = QuizResponse.Create(Guid.NewGuid(), Guid.NewGuid(), "my response");
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_CreateQuizResponseIsCalled_And_QuestionIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = QuizResponse.Create(Guid.Empty, Guid.NewGuid(), "my response");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateQuizResponseIsCalled_And_EnrollmentIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = QuizResponse.Create(Guid.NewGuid(), Guid.Empty, "my response");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateQuizResponseIsCalled_And_ResponseIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = QuizResponse.Create(Guid.NewGuid(), Guid.NewGuid(), string.Empty);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}