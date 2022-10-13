using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;

namespace SwitchSupport.Web.Areas.UserPanel.Controllers
{
    public class AccountController : UserPanelBaseController
    {
        private IStateServices _stateServices;

        public AccountController(IStateServices stateServices)
        {
            _stateServices = stateServices;
        }
        #region Ctor

        #endregion

        #region EditInfo
        [HttpGet("EditInfo")]
        public async Task<IActionResult> EditInfo()
        {
            ViewData["state"] = await _stateServices.GetAllState();
            return View(); 
        }

        public async Task<IActionResult> LoadCities(long countryId)
        {
            var res = await _stateServices.GetAllState(countryId);
            return new JsonResult(res);
        }
        #endregion
    }
}
