using Microsoft.AspNetCore.Mvc;

namespace LakeXplorerBlazor.Data
{
    public class AuthService : IAuthService
    {
        private readonly IApiService _apiService;

        public AuthService(IApiService apiService)
        {
            _apiService = apiService;
        }

        // Sends a POST request to the authentication API to log in a user.
        public async Task<TokenDTO> LoginAsync(string username, string password)
        {
            return await _apiService.HttpPOST<TokenDTO>("api/Auth/login", new { username, password });
        }
        // Sends a POST request to the authentication API to register a new user.
        public async Task<string> RegisterAsync(string Username, string Email, string Password)
        {
            return await _apiService.HttpPOST<string>("api/Auth/register", new { Username, Password, Email });
        }

    }
}
