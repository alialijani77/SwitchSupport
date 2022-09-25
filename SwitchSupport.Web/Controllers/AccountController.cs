using Microsoft.AspNetCore.Mvc;

namespace SwitchSupport.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Login
        public async Task<IActionResult> Login()
        {
            return View();
        }
        #endregion

        #region Register
        public async Task<IActionResult> Register()
        {
            return View();
        }
        #endregion
    }
}
