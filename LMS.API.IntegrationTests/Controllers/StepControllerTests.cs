using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using FluentAssertions;
using LMS.API.IntegrationTests.Base;
using LMS.App.ViewModels;
using LMS.Application.Features.Steps.Commands.CreateStep;
using LMS.Application.Features.Steps.Commands.UpdateStep;
using LMS.Domain.Entities;
using Newtonsoft.Json;

namespace LMS.API.IntegrationTests.Controllers;

public class StepControllerTests: BaseApplicationContextTests
{
    private const string RequestUri = "/api/v1/Step";
    
    [Fact]
    public async Task When_GetAllStepsQueryHandlerIsCalled_Then_Success()
    {
        // Arrange && Act
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await Client.GetAsync(RequestUri+"/Course/aedefc40-7093-4118-abc3-b2f0929353fd");

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<StepWrapper>(responseString);
        
        // Assert
        result?.Steps.Count.Should().Be(1);
    }
    
    [Fact]
    public async Task When_GetAllStepsQueryHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.GetAsync(RequestUri+"/Course/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_PostStepCommandHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var step = new CreateStepCommand
        {
            CourseId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, step);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateStepCommandResponse>(responseString);
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task When_PostStepCommandHandlerIsCalledWithWrongParameters_Then_400ShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var step = new CreateStepCommand
        {
            CourseId = Guid.Empty
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, step);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task When_PostStepCommandHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange
        var step = new CreateStepCommand
        {
            CourseId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, step);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_DeleteStepCommandHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        // Act
        var response = await Client.DeleteAsync(RequestUri+"/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task When_DeleteStepCommandHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange
        // Act
        var response = await Client.DeleteAsync(RequestUri+"/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_PutStepCommandHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var step = new CreateStepCommand()
        {
            CourseId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        
        var createResponse = await Client.PostAsJsonAsync(RequestUri, step);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateStepCommandResponse>(createResponseString);
        
        var updateStep = new UpdateStepCommand
        {
            Id = createResult.Step.Id,
            CourseId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        // Act
        var response = await Client.PutAsJsonAsync(RequestUri + $"/{createResult.Step.Id}", updateStep);

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task When_PutStepCommandHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange
        var step = new UpdateStepCommand
        {
            Id = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd"),
            CourseId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        // Act
        var response = await Client.PutAsJsonAsync(RequestUri + "/aedefc40-7093-4118-abc3-b2f0929353fd", step);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_GetStepByIdQueryHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange && Act
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var step = new CreateStepCommand
        {
            CourseId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        
        var createResponse = await Client.PostAsJsonAsync(RequestUri, step);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateStepCommandResponse>(createResponseString);
        
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await Client.GetAsync(RequestUri + $"/{createResult.Step.Id}");

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<StepDto>(responseString);
        
        // Assert
        result?.Should().NotBeNull();
    }
    
    [Fact]
    public async Task When_GetStepByIdQueryHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.GetAsync(RequestUri+"/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
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