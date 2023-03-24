using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Admin.User;

namespace SwitchSupport.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        #region ctor
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion
        public async Task<IActionResult> FilterUsers(FilterUserAdminViewModel filter)
        {
            var result = await _userService.GetFilterUserAdmin(filter);
            return View(filter);
        }
    }
}
