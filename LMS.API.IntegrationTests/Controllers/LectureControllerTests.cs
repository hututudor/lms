using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using FluentAssertions;
using LMS.API.IntegrationTests.Base;
using LMS.Application.Features.Lectures;
using LMS.Application.Features.Lectures.Commands.CreateLecture;
using LMS.Application.Features.Lectures.Commands.DeleteLecture;
using LMS.Application.Features.Lectures.Commands.UpdateLecture;
using LMS.Domain.Entities;
using Newtonsoft.Json;

namespace LMS.API.IntegrationTests.Controllers;

public class LectureControllerTests : BaseApplicationContextTests
{
    private const string RequestUri = "/api/v1/Lecture";

    [Fact]
    public async Task When_GetAllLecturesQueryHandlerIsCalled_Then_Success()
    {
        // Arrange && Act
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await Client.GetAsync(RequestUri+"/Step/aedefc40-7093-4118-abc3-b2f0929353fd");

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LectureWrapper>(responseString);
        
        // Assert
        result?.Lectures.Count.Should().Be(1);
    }
    
    [Fact]
    public async Task When_GetAllLecturesQueryHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.GetAsync(RequestUri+"/Step/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_PostLectureCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var lecture = new CreateLectureCommand
        {
            Name = "Test Lecture",
            StepId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd"),
            Url = "google.com"
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, lecture);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateLectureCommandResponse>(responseString);
        result?.Should().NotBeNull();
        result?.Lecture.Name.Should().Be(lecture.Name);
    }
    
    [Fact]
    public async Task When_PostLectureCommandHandlerIsCalledWithWrongParameters_Then_400ShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var lecture = new CreateLectureCommand
        {
            Name = "Test Lecture",
            StepId = Guid.Empty
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, lecture);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task When_PostLectureCommandHandlerIsCalledWithWrongParameters_Then_401ShouldBeReturned()
    {
        // Arrange
        var lecture = new CreateLectureCommand
        {
            Name = "Test Lecture",
            StepId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, lecture);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_UpdateLectureCommandHandlerIsCalledWithRightParameters_Then_TheEntityUpdatedShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var lecture = new CreateLectureCommand
        {
            Name = "Lecture43",
            StepId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b"),
            Url = "da2.com"
        };
        // Act
        var createResponse = await Client.PostAsJsonAsync(RequestUri, lecture);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateLectureCommandResponse>(createResponseString);
        
        var updateLecture = new UpdateLectureCommand
        {
            Id = createResult.Lecture.Id,
            Name = "Lecture43",
            StepId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b"),
            Url = "da.com"
        };
        var response = await Client.PutAsJsonAsync($"{RequestUri}/{createResult.Lecture.Id}", updateLecture);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<UpdateLectureCommandResponse>(responseString);
        
        // Assert
        result?.Lecture.Name.Should().Be(updateLecture.Name);
    }
    
    [Fact]
    public async Task When_UpdateLectureCommandHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange
        var lecture = new UpdateLectureCommand
        {
            Name = "Test Lecture",
            StepId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b"),
            Url = "da.com"
        };
        // Act
        var response = await Client.PutAsJsonAsync($"{RequestUri}/{Guid.NewGuid()}", lecture);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task When_DeleteLectureCommandHandlerIsCalledWithRightParameters_Then_Success()
    {
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var lecture = new CreateLectureCommand
        {
            StepId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b"),
            Name = "Test Lecture",
            Url = "google.com"
        };
        
        var createResponse = await Client.PostAsJsonAsync(RequestUri, lecture);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateLectureCommandResponse>(createResponseString);
        
        var response = await Client.DeleteAsync($"{RequestUri}/{createResult.Lecture.Id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task When_DeleteLectureCommandHandlerIsCalledWithWrongParameters_Then_401ShouldBeReturned()
    {
        // Arrange
        var response = await Client.DeleteAsync(RequestUri+"/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_GetLectureByIdQueryHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var lecture = new CreateLectureCommand()
        {
            Name = "Test Course",
            StepId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd"),
            Url = "da.com"
        };
        
        var createResponse = await Client.PostAsJsonAsync(RequestUri, lecture);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateLectureCommandResponse>(createResponseString);

        // Act
        var response = await Client.GetAsync($"{RequestUri}/{createResult.Lecture.Id}");
        createResponse.EnsureSuccessStatusCode();
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LectureDto>(responseString);
        
        // Assert
        result?.Name.Should().Be(createResult.Lecture.Name);
    }
    
    [Fact]
    public async Task When_GetLectureByIdQueryHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.GetAsync($"{RequestUri}/b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_GetLectureByIdQueryHandlerIsCalledWithWrongParameters_Then_404ShouldBeReturned()
    {
        // Arrange && Act
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await Client.GetAsync($"{RequestUri}/b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0b0b");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private string CreateToken()
    {
        return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
            new JwtSecurityToken(
                JwtTokenProvider.Issuer,
                JwtTokenProvider.Issuer,
                new List<Claim> { new(ClaimTypes.Role, "User"), new("department", "Lecture") },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: JwtTokenProvider.SigningCredentials
            ));
    }
}

public class LectureWrapper
{
    public List<LectureDto> Lectures { get; set; }
}