using Microsoft.AspNetCore.Mvc;

namespace LakeXplorerBlazor.Data
{
    public interface IAuthService
    {
        Task<TokenDTO> LoginAsync (string username, string password);
        Task<string> RegisterAsync(string username, string email, string password);
    }
}
