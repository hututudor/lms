using LMS.App.Contracts;
using LMS.App.Services.Responses;
using LMS.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace LMS.App.Services
{
    public class StepDataService : IStepDataService
    {
        private const string RequestUri = "api/v1/Step";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public StepDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<StepViewModel>> CreateStepAsync(StepDto stepDto)
        {
            httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, stepDto);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<StepViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<List<StepViewModel>> GetStepsAsync(Guid courseId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.GetAsync($"{RequestUri}/course/{courseId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var wrapper = JsonSerializer.Deserialize<StepWrapper>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return wrapper.Steps!;
        }
        
        public async Task<ApiResponse<StepViewModel>> DeleteStepAsync(Guid stepId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.DeleteAsync($"{RequestUri}/{stepId}");
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<StepViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
    }
}

public class StepWrapper
{
    public List<StepViewModel> Steps { get; set; }
}