using FluentAssertions;
using LMS.Domain.Entities;

namespace LMS.Core.Domain.Tests;

public class LectureTests
{
    [Fact]
    public void When_CreateLectureIsCalled_And_StepIdIsValid_And_NameIsValid_AndUrlIsValid_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = Lecture.Create(Guid.NewGuid(), "lecture 1", "https://www.youtube.com");
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_CreateLectureIsCalled_And_StepIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Lecture.Create(Guid.Empty, "lecture 1", "https://www.youtube.com");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateLectureIsCalled_And_NameIsNull_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Lecture.Create(Guid.NewGuid(), null, "https://www.youtube.com");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateLectureIsCalled_And_UrlIsNull_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Lecture.Create(Guid.NewGuid(), "lecture 1", null);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateLectureIsCalled_And_StepIdIsValid_And_NameIsValid_AndUrlIsValid_Then_SuccessIsReturned()
    {
        // Arrange
        var lecture = Lecture.Create(Guid.NewGuid(), "lecture 1", "https://www.youtube.com").Value;
        
        // Act
        var result = Lecture.Update(lecture, Guid.NewGuid(), "lecture 2", "https://www.youtube.com");
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void When_UpdateLectureIsCalled_And_StepIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange
        var lecture = Lecture.Create(Guid.NewGuid(), "lecture 1", "https://www.youtube.com").Value;
        
        // Act
        var result = Lecture.Update(lecture, Guid.Empty, "lecture 2", "https://www.youtube.com");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateLectureIsCalled_And_NameIsEmpty_Then_FailureIsReturned()
    {
        // Arrange
        var lecture = Lecture.Create(Guid.NewGuid(), "lecture 1", "https://www.youtube.com").Value;
        
        // Act
        var result = Lecture.Update(lecture, Guid.NewGuid(), null, "https://www.youtube.com");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateLectureIsCalled_And_UrlIsEmpty_Then_FailureIsReturned()
    {
        // Arrange
        var lecture = Lecture.Create(Guid.NewGuid(), "lecture 1", "https://www.youtube.com").Value;
        
        // Act
        var result = Lecture.Update(lecture, Guid.NewGuid(), "lecture 2", null);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}