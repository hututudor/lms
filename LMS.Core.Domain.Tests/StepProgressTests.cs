using FluentAssertions;
using LMS.Domain.Entities;

namespace LMS.Core.Domain.Tests;

public class StepProgressTests
{
    [Fact]
    public void When_CreateStepProgressIsCalled_And_EnrollmentIdIsValid_And_StepIdIsValid_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = StepProgress.Create(Guid.NewGuid(), Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_CreateStepProgressIsCalled_And_EnrollmentIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = StepProgress.Create(Guid.Empty, Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateStepProgressIsCalled_And_StepIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = StepProgress.Create(Guid.NewGuid(), Guid.Empty);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}