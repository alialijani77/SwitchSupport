using Microsoft.AspNetCore.Mvc;

namespace SwitchSupport.Web.Areas.UserPanel.Controllers
{
    public class HomeController : UserPanelBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
