using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Interfaces;

namespace SwitchSupport.Web.ViewComponents
{
    public class UserMainMenuBoxViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public UserMainMenuBoxViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserById(HttpContext.User.GetUserId());
            return View("UserMainMenuBox", user);
        }
    }
}
