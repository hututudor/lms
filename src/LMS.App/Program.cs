using Blazored.LocalStorage;
using LMS.App;
using LMS.App.Auth;
using LMS.App.Contracts;
using LMS.App.Services;
using LMS.TicketManagement.App.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5131/");
});

builder.Services.AddHttpClient<ICourseDataService, CourseDataService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5131/");
});

builder.Services.AddHttpClient<ILectureDataService, LectureDataService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5131/");
});

builder.Services.AddHttpClient<IStepDataService, StepDataService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5131/");
});

builder.Services.AddHttpClient<IQuizDataService, QuizDataService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5131/");
});

await builder.Build().RunAsync();