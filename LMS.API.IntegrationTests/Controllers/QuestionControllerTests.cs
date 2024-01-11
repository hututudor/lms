using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using FluentAssertions;
using LMS.API.IntegrationTests.Base;
using LMS.Application.Features.Questions;
using LMS.Application.Features.Questions.Commands.CreateQuestion;
using LMS.Application.Features.Questions.Commands.DeleteQuestion;
using LMS.Application.Features.Questions.Commands.UpdateQuestion;
using Newtonsoft.Json;

namespace LMS.API.IntegrationTests.Controllers;

public class QuestionControllerTests: BaseApplicationContextTests
{
    private const string RequestUri = "/api/v1/Question";
    
    [Fact]
    public async Task When_PostQuestionCommandHandlerIsCalledWithWrongParameters_Then_400ShouldBeReturned()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var question = new CreateQuestionCommand
        {
            QuizId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd"),
            Description = "Test Question",
            Answer = "Test Answer",
            Options = new List<string>()
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, question);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task When_PostQuestionCommandHandlerIsCalledWithWrongParameters_Then_401ShouldBeReturned()
    {
        // Arrange
        var question = new CreateQuestionCommand
        {
            QuizId = Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd"),
            Description = "Test Question",
            Answer = "Test Answer",
            Options = new List<string>()
        };
        // Act
        var response = await Client.PostAsJsonAsync(RequestUri, question);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_DeleteQuestionCommandHandlerIsCalledWithRightParameters_Then_Success()
    {
        // Arrange
        string token = CreateToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        // Act
        var response = await Client.DeleteAsync(RequestUri + "/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task When_DeleteQuestionCommandHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Act
        var response = await Client.DeleteAsync(RequestUri + "/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task When_GetQuestionByIdQueryHandlerIsCalledWithoutAuth_Then_401ShouldBeReturned()
    {
        // Arrange && Act
        var response = await Client.GetAsync(RequestUri+"/aedefc40-7093-4118-abc3-b2f0929353fd");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task When_GetQuestionByIdQueryHandlerIsCalledWithWrongParameters_Then_401ShouldBeReturned()
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
