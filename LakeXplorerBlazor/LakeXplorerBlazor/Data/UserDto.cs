using System.ComponentModel.DataAnnotations;

namespace LakeXplorerBlazor.Data
{
    // Represents a Data Transfer Object (DTO) for user registration and authentication.
    public class UserDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
