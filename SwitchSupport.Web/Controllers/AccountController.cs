using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;

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
        #endregion
    }
}
