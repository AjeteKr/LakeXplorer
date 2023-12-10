using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LakeXplorerBlazor.Data;

namespace LakeXplorerBlazor.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        private IAuthService _authService { get; set; }

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect("~/");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tokenObject = await _authService.LoginAsync(Input.Username, Input.Password);
                    if (tokenObject != null)
                    {
                        var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        claimsIdentity.AddClaim(new Claim("Token", tokenObject.Token));
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, Input.Username)); 
                        var principal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        return LocalRedirect("~/");
                    }
                    TempData["ErrorMessage"] = "Wrong! Try again.";
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