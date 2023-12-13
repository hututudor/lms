using LMS.App.ViewModels;

namespace LMS.App.Contracts;

public interface IAuthenticationService
{
    Task Login(LoginViewModel loginRequest);
    Task Register(RegisterViewModel registerRequest);
    Task Logout();
}
