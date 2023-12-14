using LMS.App.Contracts;
using LMS.App.Services.Responses;
using LMS.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace LMS.App.Services
{
    public class QuizDataService : IQuizDataService
    {
        private const string RequestUri = "api/v1/Quiz";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public QuizDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<QuizViewModel>> CreateQuizAsync(QuizDto quizDto)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, quizDto);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<QuizViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponse<QuizViewModel>> UpdateQuizAsync(QuizViewModel quizViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PutAsJsonAsync($"{RequestUri}/{quizViewModel.Id}", quizViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<QuizViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<QuizViewModel> GetQuizAsync(Guid quizId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.GetAsync($"{RequestUri}/{quizId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var quiz = JsonSerializer.Deserialize<QuizViewModel>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return quiz!;
        }

        public async Task<List<QuizViewModel>> GetQuizzesAsync(Guid stepId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result =
                await httpClient.GetAsync($"{RequestUri}/Step/{stepId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var quizWrapper = JsonSerializer.Deserialize<QuizWrapper>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return quizWrapper.Quizzes!;
        }
    }
}

public class QuizWrapper
{
    public List<QuizViewModel> Quizzes { get; set; }
}
