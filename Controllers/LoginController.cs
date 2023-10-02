using Enquiry.Web.Dtos;
using Enquiry.Web.Helpers;
using Enquiry.Web.Models;
using Enquiry.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Enquiry.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly Services.AuthenticationService _authService;

        public LoginController(Services.AuthenticationService authService)
        {
            this._authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthenticateAsync(UserCredentials userCredentials)
        {
            try
            {
                string securityToken = _authService.Authenticate(userCredentials);

                var token = securityToken;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var principal = new ClaimsPrincipal(new ClaimsIdentity(jwtSecurityToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme));

                await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7),
            });

                return RedirectToAction("Index", "Enquiry");
            }
            catch (InvalidCredentialsException)
            {
                this.Unauthorized();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
