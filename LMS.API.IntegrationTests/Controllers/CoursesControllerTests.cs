using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using FluentAssertions;
using LMS.API.IntegrationTests.Base;
using LMS.Application.Features.Courses;
using LMS.Application.Features.Courses.Commands.CreateCourse;
using LMS.Application.Features.Courses.Commands.DeleteCourse;
using LMS.Application.Features.Courses.Commands.UpdateCourse;
using Newtonsoft.Json;

namespace LMS.API.IntegrationTests.Controllers;

public class CoursesControllerTests: BaseApplicationContextTests
{
    private const string RequestUri = "/api/v1/Course";

    [Fact]
    public async Task When_GetAllCategoriesQueryHandlerIsCalled_Then_Success()
    {
        // Arrange && Act
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await Client.GetAsync(RequestUri);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CourseWrapper>(responseString);
        
        // Assert
        result?.Courses.Count.Should().Be(2);
    }
    
    [Fact]
    public async Task When_GetAllCategoriesQueryHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.GetAsync(RequestUri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_PostCategoryCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var course = new CreateCourseCommand
        {
            Name = "Test Course",
            UserId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, course);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateCourseCommandResponse>(responseString);
        result?.Should().NotBeNull();
        result?.Course.Name.Should().Be(course.Name);
    }
    
    [Fact]
    public async Task When_PostCategoryCommandHandlerIsCalledWithWrongParameters_Then_400ShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var course = new CreateCourseCommand
        {
            Name = String.Empty,
            UserId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, course);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task When_PostCategoryCommandHandlerIsCalledWithWrongParameters_Then_401ShouldBeReturned()
    {
        // Arrange
        var course = new CreateCourseCommand
        {
            Name = "Test Course",
            UserId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, course);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task When_GetCourseByIdQueryHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var course = new CreateCourseCommand
        {
            Name = "Test Course",
            UserId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        
        var createResponse = await Client.PostAsJsonAsync(RequestUri, course);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateCourseCommandResponse>(createResponseString);

        // Act
        var response = await Client.GetAsync($"{RequestUri}/{createResult.Course.Id}");

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CourseDto>(responseString);
        
        // Assert
        result?.Name.Should().Be(createResult.Course.Name);
    }
    
    [Fact]
    public async Task When_GetCourseByIdQueryHandlerIsCalledWrongRightParameters_Then_404ShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        // Act
        var response = await Client.GetAsync($"{RequestUri}/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task When_GetCourseByIdQueryHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.GetAsync($"{RequestUri}/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_UpdateCourseCommandHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var course = new CreateCourseCommand
        {
            Name = "Test Course",
            UserId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        
        var createResponse = await Client.PostAsJsonAsync(RequestUri, course);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateCourseCommandResponse>(createResponseString);

        var updateCourse = new UpdateCourseCommand
        {
            Id = createResult.Course.Id,
            Name = "Updated Test Course",
            UserId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        
        // Act
        var response = await Client.PutAsJsonAsync($"{RequestUri}/{createResult.Course.Id}", updateCourse);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<UpdateCourseCommandResponse>(responseString);
        
        // Assert
        result?.Course.Name.Should().Be(updateCourse.Name);
    }
    
    [Fact]
    public async Task When_UpdateCourseCommandHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange
        var updateCourse = new UpdateCourseCommand
        {
            Id = Guid.NewGuid(),
            Name = "Updated Test Course",
            UserId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        
        // Act
        var response = await Client.PutAsJsonAsync($"{RequestUri}/{Guid.NewGuid()}", updateCourse);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_DeleteCourseCommandHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var course = new CreateCourseCommand
        {
            Name = "Test Course",
            UserId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        
        var createResponse = await Client.PostAsJsonAsync(RequestUri, course);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateCourseCommandResponse>(createResponseString);

        // Act
        var response = await Client.DeleteAsync($"{RequestUri}/{createResult.Course.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task When_DeleteCourseCommandHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.DeleteAsync($"{RequestUri}/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    private string CreateToken()
    {
        return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
            new JwtSecurityToken(
                JwtTokenProvider.Issuer,
                JwtTokenProvider.Issuer,
                new List<Claim> { new(ClaimTypes.Role, "User"), new("department", "Security") },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: JwtTokenProvider.SigningCredentials
            ));
    }
}

public class CourseWrapper
{
    public List<CourseDto> Courses { get; set; }
}
