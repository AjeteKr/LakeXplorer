using LakeXplorerBlazor.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LakeXplorerBlazor.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        private IAuthService _authService { get; set; }

        public RegisterModel(IAuthService authService)
        {
            _authService = authService;
        }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var registrationResult = await _authService.RegisterAsync(Input.Username, Input.Email, Input.Password);

                    if (registrationResult == null)
                    {
                        TempData["ErrorMessage"] = registrationResult;
                    }
                    else
                    {
                        return LocalRedirect("/");
                    }
                }
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error Server...";
                return Page();
            }
        }
    }
}
