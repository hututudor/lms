using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using FluentAssertions;
using LMS.API.IntegrationTests.Base;
using LMS.Application.Features.Quizzes;
using LMS.Application.Features.Quizzes.Commands.CreateQuiz;
using LMS.Application.Features.Quizzes.Commands.DeleteQuiz;
using LMS.Application.Features.Quizzes.Commands.UpdateQuiz;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace LMS.API.IntegrationTests.Controllers;

public class QuizzesControllerTests : BaseApplicationContextTests
{
    private const string RequestUri = "/api/v1/Quiz";
    [Fact]
    public async Task When_GetAllQuizzesQueryHandlerIsCalled_Then_Success()
    {
        // Arrange && Act
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await Client.GetAsync(RequestUri+"/Step/aedefc40-7093-4118-abc3-b2f0929353fd");

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<QuizWrapper>(responseString);
        
        // Assert
        result?.Quizzes.Count.Should().Be(2);
    }
    
    [Fact]
    public async Task When_GetAllQuizzesQueryHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.GetAsync(RequestUri+"/Step/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_PostQuizCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var quiz = new CreateQuizCommand
        {
            Name = "Test Quiz",
            StepId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, quiz);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateQuizCommandResponse>(responseString);
        result?.Should().NotBeNull();
        result?.Quiz.Name.Should().Be(quiz.Name);
    }
    
    [Fact]
    public async Task When_PostQuizCommandHandlerIsCalledWithWrongParameters_Then_400ShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var quiz = new CreateQuizCommand
        {
            Name = "Test Quiz"
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, quiz);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task When_PostQuizCommandHandlerIsCalledWithWrongParameters_Then_401ShouldBeReturned()
    {
        // Arrange
        var quiz = new CreateQuizCommand
        {
            Name = "Test Quiz",
            StepId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, quiz);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_UpdateQuizCommandHandlerIsCalledWithRightParameters_Then_TheEntityUpdatedShouldBeReturned()
    {
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var quiz = new CreateQuizCommand()
        {
            Name = "Quiz1",
            StepId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b")
        };
        // Act
        var createResponse = await Client.PostAsJsonAsync(RequestUri, quiz);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateQuizCommandResponse>(createResponseString);
        
        var updateQuiz = new UpdateQuizCommand
        {
            Id = createResult.Quiz.Id,
            Name = "Quiz13",
            StepId = Guid.Parse("b2d9a9a0-0f1a-4f6a-8f7a-0e2a9a9b0a0b"),
        };
        var response = await Client.PutAsJsonAsync($"{RequestUri}/{createResult.Quiz.Id}", updateQuiz);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<UpdateQuizCommandResponse>(responseString);
        
        // Assert
        result?.Quiz.Name.Should().Be(updateQuiz.Name);
    }
    
    [Fact]
    public async Task When_UpdateQuizCommandHandlerIsCalledWithWrongParameters_Then_401ShouldBeReturned()
    {
        // Arrange
        var quiz = new UpdateQuizCommand
        {
            Name = "Test Quiz",
            StepId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        // Act
        var response = await Client.PutAsJsonAsync($"{RequestUri}/{Guid.NewGuid()}", quiz);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_DeleteQuizCommandHandlerIsCalledWithRightParameters_Then_Success()
    {
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var quiz = new CreateQuizCommand
        {
            Name = "Test Quiz",
            StepId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        
        var createResponse = await Client.PostAsJsonAsync(RequestUri, quiz);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateQuizCommandResponse>(createResponseString);
        
        var response = await Client.DeleteAsync($"{RequestUri}/{createResult.Quiz.Id}");

        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task When_DeleteQuizCommandHandlerIsCalledWithWrongParameters_Then_401ShouldBeReturned()
    {
        // Arrange
        var response = await Client.DeleteAsync(RequestUri+"/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_GetQuizByIdQueryHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange && Act
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var quiz = new CreateQuizCommand()
        {
            Name = "Test Quiz",
            StepId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")
        };
        
        var createResponse = await Client.PostAsJsonAsync(RequestUri, quiz);
        var createResponseString = await createResponse.Content.ReadAsStringAsync();
        var createResult = JsonConvert.DeserializeObject<CreateQuizCommandResponse>(createResponseString);
        
        var response = await Client.GetAsync($"{RequestUri}/{createResult.Quiz.Id}");
        createResponse.EnsureSuccessStatusCode();
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<QuizDto>(responseString);
        
        // Assert
        result?.Name.Should().Be(createResult.Quiz.Name);
    }
    
    [Fact]
    public async Task When_GetQuizByIdQueryHandlerIsCalledWithoutAuthParameters_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.GetAsync($"{RequestUri}/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_GetQuizByIdQueryHandlerIsCalledWithWrongParameters_Then_404ShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var invalidQuizId = Guid.NewGuid(); // Assuming this ID does not exist in the database

        // Act
        var response = await Client.GetAsync($"{RequestUri}/{invalidQuizId}");

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

public class QuizWrapper
{
    public List<QuizDto> Quizzes { get; set; }
}