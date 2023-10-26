using Microsoft.AspNetCore.Mvc;

namespace LakeXplorerBlazor.Data
{
    // Interface for handling user authentication-related operations.
    public interface IAuthService
    {
        // Attempts to log in a user using the provided credentials.
        Task<TokenDTO> LoginAsync (string username, string password);
        
        // Registers a new user with the provided information.
        Task<string> RegisterAsync(string username, string email, string password);
    }
}
