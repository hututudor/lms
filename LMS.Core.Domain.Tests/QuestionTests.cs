using FluentAssertions;
using LMS.Domain.Entities;

namespace LMS.Core.Domain.Tests;

public class QuestionTests
{
    [Fact]
    public void
        When_CreateCourseIsCalled_And_QuizIdIsValid_And_DescriptionIsValid_And_AnswerIsValid_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = Question.Create(Guid.NewGuid(), "question 1", "answer 1", new List<string> {"option 1"});
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_CreateCourseIsCalled_And_QuizIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Question.Create(Guid.Empty, "question 1", "answer 1", new List<string> {"option 1"});
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateCourseIsCalled_And_DescriptionIsNull_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Question.Create(Guid.NewGuid(), null, "answer 1", new List<string> {"option 1"});
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateCourseIsCalled_And_AnswerIsNull_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Question.Create(Guid.NewGuid(), "question 1", null, new List<string> {"option 1"});
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateCourseIsCalled_And_QuizIdIsValid_And_DescriptionIsValid_And_AnswerIsValid_Then_SuccessIsReturned()
    {
        // Arrange
        var question = Question.Create(Guid.NewGuid(), "question 1", "answer 1", new List<string> {"option 1"}).Value;
        
        // Act
        var result = Question.Update(question, Guid.NewGuid(), "question 2", "answer 2", new List<string> {"option 2"});
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_UpdateCourseIsCalled_And_QuizIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange
        var question = Question.Create(Guid.NewGuid(), "question 1", "answer 1", new List<string> {"option 1"}).Value;
        
        // Act
        var result = Question.Update(question, Guid.Empty, "question 2", "answer 2", new List<string> {"option 2"});
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateCourseIsCalled_And_DescriptionIsNull_Then_FailureIsReturned()
    {
        // Arrange
        var question = Question.Create(Guid.NewGuid(), "question 1", "answer 1", new List<string> {"option 1"}).Value;
        
        // Act
        var result = Question.Update(question, Guid.NewGuid(), null, "answer 2", new List<string> {"option 2"});
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateCourseIsCalled_And_AnswerIsNull_Then_FailureIsReturned()
    {
        // Arrange
        var question = Question.Create(Guid.NewGuid(), "question 1", "answer 1", new List<string> {"option 1"}).Value;
        
        // Act
        var result = Question.Update(question, Guid.NewGuid(), "question 2", null, new List<string> {"option 2"});
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }

}