using Blazored.LocalStorage;
using LMS.App.Contracts;
using System.IdentityModel.Tokens.Jwt;

namespace LMS.App.Services
{

    public class TokenService : ITokenService
    {
        private const string TOKEN = "token";
        private readonly ILocalStorageService localStorageService;

        public TokenService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task SetTokenAsync(string token)
        {
            await localStorageService.SetItemAsync(TOKEN, token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await localStorageService.GetItemAsync<string>(TOKEN);
        }
        
        public async Task<string> GetUserIdAsync()
        {
            var token = await GetTokenAsync();
            if (String.IsNullOrWhiteSpace(token))
            {
                return null;
            }
            
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var userId = decodedToken.Claims.First(claim => claim.Type == "jti").Value;
            return userId;
        }

        public async Task RemoveTokenAsync()
        {
            await localStorageService.RemoveItemAsync(TOKEN);
        }
    }
}
