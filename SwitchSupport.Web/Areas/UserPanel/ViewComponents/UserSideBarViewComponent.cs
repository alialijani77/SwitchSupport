using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Interfaces;

namespace SwitchSupport.Web.Areas.UserPanel.ViewComponents
{
    public class UserSideBarViewComponent : ViewComponent
    {
        private IUserService _userService;
        #region ctor
        public UserSideBarViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.GetUserId();
            var user = await _userService.GetUserById(userId);
            return View("UserSideBar",user);
        }
    }
}
