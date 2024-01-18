using LMS.App.Contracts;
using LMS.App.Services.Responses;
using LMS.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace LMS.App.Services
{
    public class LectureDataService : ILectureDataService
    {
        private const string RequestUri = "api/v1/Lecture";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public LectureDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        
        public async Task<ApiResponse<LectureViewModel>> CreateLectureAsync(LectureDto lectureDto)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, lectureDto);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LectureViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
        
        public async Task<ApiResponse<LectureViewModel>> UpdateLectureAsync(LectureViewModel lectureViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PutAsJsonAsync($"{RequestUri}/{lectureViewModel.Id}", lectureViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LectureViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
        
        public async Task<LectureViewModel> GetLectureAsync(Guid lectureId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var requestUrl = $"{RequestUri}/{lectureId}";
            Console.WriteLine($"Request URL: {requestUrl}");
            
            var result = await httpClient.GetAsync($"{RequestUri}/{lectureId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var lecture = JsonSerializer.Deserialize<LectureViewModel>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return lecture!;
        }
        
        public async Task<List<LectureViewModel>> GetLecturesAsync(Guid stepId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.GetAsync($"{RequestUri}/step/{stepId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var wrapper = JsonSerializer.Deserialize<LectureWrapper>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return wrapper.Lectures!;
        }

        public async Task<ApiResponse<LectureViewModel>> DeleteLectureAsync(Guid lectureId)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.DeleteAsync($"{RequestUri}/{lectureId}");
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LectureViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;

        }
    }
}

public class LectureWrapper
{
    public List<LectureViewModel> Lectures { get; set; }
}
