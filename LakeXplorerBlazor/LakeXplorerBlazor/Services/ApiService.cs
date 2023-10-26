using System.Net.Http.Headers;
using System.Net;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace LakeXplorerBlazor
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7280/");

        }

        public async Task<T> HttpGET<T>(string url)
        {
            
            var tokenPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            var token = tokenPrincipal?.Claims.FirstOrDefault(f => f.Type == "Token");
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(),
                    $"Bearer {token.Value}");
            }

            using var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default;
        }

        public async Task<T> HttpPOST<T>(string url, object postData)
        {
            var token = ClaimsPrincipal.Current?.Claims.FirstOrDefault(f => f.Type == "Token");
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(),
                    $"Bearer {token.Value}");
            }

            using var response = await _httpClient.PostAsJsonAsync(url, postData);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default;
        }

    }
}
