
using System.Net.Http;
using System.Threading;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
namespace LakeXplorerBlazor
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7280/");
        }
        public async Task<T> HttpGET<T>(string url)
        {
            var tokenPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            var token = tokenPrincipal?.Claims.FirstOrDefault(f => f.Type == "Token");
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
            }
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            return default;
        }


        public async Task<bool> UpdateLikeStatus(int lakeId, bool isLiked)
        {
            try
            {
                var postData = new { LakeId = lakeId, IsLiked = isLiked };
                var response = await _httpClient.PostAsJsonAsync($"api/{lakeId}", postData);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> HttpPOST<T>(string url, object postData)
        {
            try
            {

           
            var token = ClaimsPrincipal.Current?.Claims.FirstOrDefault(f => f.Type == "Token");
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
            }
            var response = await _httpClient.PostAsJsonAsync(url, postData);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            return default;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
