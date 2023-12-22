using FluentAssertions;
using LMS.Domain.Entities;

namespace LMS.Core.Domain.Tests;

public class CourseTests
{
    [Fact]
    public void When_CreateCourseIsCalled_And_NameIsValid_And_UserIdIsValid_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = Course.Create("course 1", Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_CreateCourseIsCalled_And_NameIsNull_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Course.Create(null, Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateCourseIsCalled_And_UserIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Course.Create("course 1", Guid.Empty);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateCourseIsCalled_And_NameIsValid_And_UserIdIsValid_Then_SuccessIsReturned()
    {
        // Arrange
        var course = Course.Create("course 1", Guid.NewGuid()).Value;
        
        // Act
        var result = Course.Update(course, "course 2", Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_UpdateCourseIsCalled_And_NameIsNull_Then_FailureIsReturned()
    {
        // Arrange
        var course = Course.Create("course 1", Guid.NewGuid()).Value;
        
        // Act
        var result = Course.Update(course, null, Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateCourseIsCalled_And_UserIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange
        var course = Course.Create("course 1", Guid.NewGuid()).Value;
        
        // Act
        var result = Course.Update(course, "course 2", Guid.Empty);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}