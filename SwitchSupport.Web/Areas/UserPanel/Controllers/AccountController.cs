using Microsoft.AspNetCore.Mvc;

namespace SwitchSupport.Web.Areas.UserPanel.Controllers
{
    public class AccountController : UserPanelBaseController
    {
        #region Ctor

        #endregion

        #region EditInfo
        public async Task<IActionResult> EditInfo()
        {
            return View(); 
        }
        #endregion
    }
}
