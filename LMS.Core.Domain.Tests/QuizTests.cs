using FluentAssertions;
using LMS.Domain.Entities;

namespace LMS.Core.Domain.Tests;

public class QuizTests
{
    [Fact]
    public void When_CreateQuizIsCalled_And_NameIsValid_And_StepIdIsValid_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = Quiz.Create("quiz 1", Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_CreateQuizIsCalled_And_NameIsNull_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Quiz.Create(null, Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateQuizIsCalled_And_StepIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Quiz.Create("quiz 1", Guid.Empty);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateQuizIsCalled_And_NameIsValid_And_StepIdIsValid_Then_SuccessIsReturned()
    {
        // Arrange
        var quiz = Quiz.Create("quiz 1", Guid.NewGuid()).Value;
        
        // Act
        var result = Quiz.Update(quiz, Guid.NewGuid(), "quiz 2");
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_UpdateQuizIsCalled_And_NameIsNull_Then_FailureIsReturned()
    {
        // Arrange
        var quiz = Quiz.Create("quiz 1", Guid.NewGuid()).Value;
        
        // Act
        var result = Quiz.Update(quiz, Guid.NewGuid(), null);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateQuizIsCalled_And_StepIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange
        var quiz = Quiz.Create("quiz 1", Guid.NewGuid()).Value;
        
        // Act
        var result = Quiz.Update(quiz, Guid.Empty, "quiz 2");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}