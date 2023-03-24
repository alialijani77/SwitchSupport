using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.UserPanel.Account;

namespace SwitchSupport.Web.Areas.UserPanel.Controllers
{
    public class AccountController : UserPanelBaseController
    {

        #region Ctor
        private IStateServices _stateServices;
        private readonly IUserService _userService;
        public AccountController(IStateServices stateServices, IUserService userService)
        {
            _stateServices = stateServices;
            _userService = userService;
        }
        #endregion

        #region EditInfo
        [HttpGet("EditInfo")]
        public async Task<IActionResult> EditInfo()
        {
            ViewData["state"] = await _stateServices.GetAllState();
            var edituser = await _userService.GetEditUser(User.GetUserId());
            if(edituser.CountryId.HasValue)
            {
                ViewData["cities"] = await _stateServices.GetAllState(edituser.CountryId.Value);
            }
            return View(edituser);
        }

        [HttpPost("EditInfo"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfo(EditUserViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.EditUserInfo(editUser, HttpContext.User.GetUserId());
                switch (res)
                {
                    case ResultEditInfo.success:
                        TempData[SuccessMessage] = "عملیات با موفقیت انجام شد.";
                        return RedirectToAction("EditInfo", "Account", new { area = "UserPanel" });
                    case ResultEditInfo.notvialid:
                        TempData[ErrorMessage] = "تاریخ وارد شده صحیح نمی باشد.";
                        break;
                }                                                           
                }
                
            ViewData["state"] = await _stateServices.GetAllState();
            if (editUser.CountryId.HasValue)
            {
                ViewData["cities"] = await _stateServices.GetAllState(editUser.CountryId.Value);
            }
            return View(editUser);
        }

        public async Task<IActionResult> LoadCities(long countryId)
        {
            var res = await _stateServices.GetAllState(countryId);
            return new JsonResult(res);
        }
        #endregion
    }
}
