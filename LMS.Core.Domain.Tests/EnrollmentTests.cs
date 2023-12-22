using FluentAssertions;
using LMS.Domain.Entities;

namespace LMS.Core.Domain.Tests;

public class EnrollmentTests
{
    [Fact]
    public void When_CreateEnrollmentIsCalled_And_UserIdIsValid_And_CourseIdIsValid_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = Enrollment.Create(Guid.NewGuid(), Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_CreateEnrollmentIsCalled_And_UserIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Enrollment.Create(Guid.Empty, Guid.NewGuid());
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_CreateEnrollmentIsCalled_And_CourseIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Enrollment.Create(Guid.NewGuid(), Guid.Empty);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateEnrollmentIsCalled_And_UserIdIsValid_And_CourseIdIsValid_Then_SuccessIsReturned()
    {
        // Arrange
        var enrollment = Enrollment.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
        
        // Act
        var result = Enrollment.Update(enrollment, Guid.NewGuid(), Guid.NewGuid(), DateTime.Now);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void When_UpdateEnrollmentIsCalled_And_UserIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange
        var enrollment = Enrollment.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
        
        // Act
        var result = Enrollment.Update(enrollment, Guid.Empty, Guid.NewGuid(), DateTime.Now);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void When_UpdateEnrollmentIsCalled_And_CourseIdIsEmpty_Then_FailureIsReturned()
    {
        // Arrange
        var enrollment = Enrollment.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
        
        // Act
        var result = Enrollment.Update(enrollment, Guid.NewGuid(), Guid.Empty, DateTime.Now);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}