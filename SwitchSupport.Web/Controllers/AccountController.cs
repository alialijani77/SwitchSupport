using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Account;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Login(string ReturnUrl = "")
        {
            var login = new LoginViewModel();
            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                login.ReturnUrl = ReturnUrl;
            }
            return View(login);
        }

        [HttpPost("Login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
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
                    if (!string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return Redirect(login.ReturnUrl);
                    }

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

        #region Email Activation
        [HttpGet("Email-Activation/{activationcode}")]
        public async Task<IActionResult> EmailActivation(string activationcode)
        {
            var result = await _userService.EmailActivation(activationcode);
            if (result)
            {
                TempData[SuccessMessage] = "حساب کاربری با موفقیت فعال شد.";
            }
            else
            {
                TempData[ErrorMessage] = "متاسفانه مشکلی در فعال سازی حساب کاربری رخ داده است.";
            }
            return Redirect("/");
        }
        #endregion

        #region ForgotPassword
        [HttpGet("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }
            var result = await _userService.CheckForForgotPassword(forgotPassword);
            switch (result)
            {
                case ForgotPasswordResult.NotFound:
                    TempData[ErrorMessage] = "کاربری با ایمیل وارد شده یافت نشد.";
                    break;
                case ForgotPasswordResult.IsBan:
                    TempData[InfoMessage] = "کاربر مسدود می باشد.";
                    break;
                case ForgotPasswordResult.Success:
                    TempData[InfoMessage] = "ایمیل جهت بازیابی پسورد ارسال شد.";
                    return RedirectToAction("Login", "Account");
            }
            return View(forgotPassword);
        }
        #endregion

        #region Reset-Password
        [HttpGet("Reset-Password/{activationcode}")]
        public async Task<IActionResult> ResetPassword(string activationcode)
        {
            if (await _userService.GetUserByActivationCode(activationcode) == null)
            {
                return NotFound();
            }
            var resetpassword = new ResetPasswordViewModel();
            resetpassword.activationCode = activationcode;
            return View(resetpassword);
        }

        [HttpPost("Reset-Password/{activationcode}")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if(!ModelState.IsValid)
            {
                return View(resetPassword);
            }
            var result = await _userService.ResetPassword(resetPassword);
            switch (result)
            {
                case ResetPasswordResult.NotFound:
                    TempData[ErrorMessage] = "کاربری یافت نشد.";
                    break;               
                case ResetPasswordResult.Success:
                    TempData[InfoMessage] = "پسورد با موفقیت تغییر یافت.";
                    return RedirectToAction("Login", "Account");
            }
            return View(resetPassword);
        }
        #endregion

        #region ChangePassword
        [HttpGet("ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
        {
            return View();
        }
        #endregion
    }
}
