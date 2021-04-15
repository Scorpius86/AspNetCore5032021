using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Client.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {                
        [HttpPost]
        public IActionResult SignIn(string returnUrl)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Challenge(new AuthenticationProperties { RedirectUri = returnUrl },OpenIdConnectDefaults.AuthenticationScheme);
            }
            return Redirect(returnUrl);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignOut(string returnUrl)
        {
            return new SignOutResult(new[] {
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme
            }, new AuthenticationProperties { RedirectUri = returnUrl});
        }
    }
}
