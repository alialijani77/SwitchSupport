using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Implementions.SiteSettings;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Admin.User;
using SwitchSupport.Web.ActionFilters;

namespace SwitchSupport.Web.Areas.Admin.Controllers
{
    [PermissionChecker(2)]
    public class UserController : AdminBaseController
    {
        #region ctor
        private readonly IStateServices _stateServices;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IStateServices stateServices)
        {
            _userService = userService;
            _stateServices = stateServices;

        }
        #endregion
        public async Task<IActionResult> FilterUsers(FilterUserAdminViewModel filter)
        {
            var result = await _userService.GetFilterUserAdmin(filter);
            return View(filter);
        }

        #region EditInfo
        [HttpGet]
        public async Task<IActionResult> EditInfoUserAdmin(long userId)
        {
            var res = await _stateServices.GetAllState();
            ViewData["States"] = await _stateServices.GetAllState();
            var edituser = await _userService.GetEditUserAdmin(User.GetUserId());
            if (edituser == null) return NotFound();
            if (edituser.CountryId.HasValue)
            {
                ViewData["Cities"] = await _stateServices.GetAllState(edituser.CountryId.Value);
            }
            return View(edituser);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfoUserAdmin(EditUserAdminViewModel editUser)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "اطلاعات وارد شده معتبر نمی باشد.";
                ViewData["States"] = await _stateServices.GetAllState();
                if (editUser.CountryId.HasValue)
                {
                    ViewData["Cities"] = await _stateServices.GetAllState(editUser.CountryId.Value);
                }
                return View(editUser);
            }
            var result = await _userService.EditUserAdmin(editUser);
            switch (result)
            {
                case EditUserAdminResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("FilterUsers", "User", new { area = "Admin" });
                case EditUserAdminResult.NotValidEmail:
                    ModelState.AddModelError("Email", "ایمیل وارد شده از قبل موجود است");
                    break;
                case EditUserAdminResult.UserNotFound:
                    TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد";
                    return RedirectToAction("FilterUsers", "User", new { area = "Admin" });
            }
            ViewData["States"] = await _stateServices.GetAllState();
            if (editUser.CountryId.HasValue)
            {
                ViewData["Cities"] = await _stateServices.GetAllState(editUser.CountryId.Value);
            }
            return View(editUser);
        }
        #endregion
    }
}
