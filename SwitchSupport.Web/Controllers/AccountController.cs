using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Account;

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
