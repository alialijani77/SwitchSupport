using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Account;
using System.Security.Claims;

namespace SwitchSupport.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        #region ctor
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion
        #region Login
        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost("Login"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }
            var result = await _userService.CheckForLogin(login);
            switch (result)
            {
                case LoginResult.NoExists:
                    TempData[ErrorMessage] = "کاربری با این مشخصات یافت نشد.";
                    break;
                case LoginResult.IsDelete:
                    TempData[ErrorMessage] = "کاربری با این مشخصات یافت نشد.";
                    break;
                case LoginResult.IsBan:
                    TempData[ErrorMessage] = "کاربر مسدود می باشد. با ما تماس بگیرید.";
                    break;
                case LoginResult.IsNotActive:
                    TempData[ErrorMessage] = "خواهشمند است حساب خود را فعال نمایید.";
                    break;
                case LoginResult.Success:
                    var user = await _userService.GetUserByEmail(login.Email);
                    #region Login User

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties { IsPersistent = login.RememberMe };

                    await HttpContext.SignInAsync(principal, properties);

                    #endregion
                    TempData[SuccessMessage] = "خوش آمدید";
                    return Redirect("/");
            }
            return View(login);
        }
        #endregion

        #region Logout
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        #endregion

        #region Register
        [HttpGet("Register")]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost("Register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            var result = await _userService.RegisterUser(register);

            switch (result)
            {
                case RegisterResult.EmailExists:
                    TempData[ErrorMessage] = "ایمیل وارد شده تکراری می باشد.";
                    break;
                case RegisterResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد.";
                    return RedirectToAction("Login", "Account");
            }
            return View(register);
        }
        #endregion
    }
}
