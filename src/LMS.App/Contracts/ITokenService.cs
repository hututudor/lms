namespace LMS.App.Contracts;

public interface ITokenService
{
    Task<string> GetTokenAsync();
    Task<string> GetUserIdAsync();
    Task RemoveTokenAsync();
    Task SetTokenAsync(string token);
}

