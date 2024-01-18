using LMS.App.Contracts;
using LMS.App.Services.Responses;
using LMS.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace LMS.App.Services
{
    public class CourseDataService : ICourseDataService
    {
        private const string RequestUri = "api/v1/Course";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public CourseDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<CourseViewModel>> CreateCourseAsync(CourseDto courseDto)
        {
            courseDto.UserId = new Guid(await tokenService.GetUserIdAsync());
            httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, courseDto);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<CourseViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
        
        public async Task<ApiResponse<CourseViewModel>> UpdateCourseAsync(CourseViewModel courseDto)
        {
            httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PutAsJsonAsync($"{RequestUri}/{courseDto.Id}", courseDto);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<CourseViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
        
        public async Task<ApiResponse<CourseViewModel>> DeleteCourseAsync(Guid courseId)
        {
            httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.DeleteAsync($"{RequestUri}/{courseId}");
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<CourseViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
        
        public async Task<CourseViewModel> GetCourseAsync(string courseId)
        {
            httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.GetAsync($"{RequestUri}/{courseId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var course = JsonSerializer.Deserialize<CourseViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return course!;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var wrapper = JsonSerializer.Deserialize<CourseWrapper>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return wrapper.Courses!;
        }
    }
}

public class CourseWrapper
{
    public List<CourseViewModel> Courses { get; set; }
}
