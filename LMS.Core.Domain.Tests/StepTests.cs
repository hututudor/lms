using FluentAssertions;
using LMS.Domain.Entities;

namespace LMS.Core.Domain.Tests;

public class StepTests
{
    [Fact]
    public void When_CreateStepIsCalled_And_CourseIdIsValid_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = Step.Create(Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_CreateStepIsCalled_And_CourseIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Step.Create(Guid.Empty);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateStepIsCalled_And_CourseIdIsValid_Then_SuccessIsReturned()
    {
        // Arrange
        var step = Step.Create(Guid.NewGuid()).Value;
        
        // Act
        var result = Step.Update(step, Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_UpdateStepIsCalled_And_CourseIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange
        var step = Step.Create(Guid.NewGuid()).Value;
        
        // Act
        var result = Step.Update(step, Guid.Empty);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}