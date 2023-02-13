using Microsoft.AspNetCore.Mvc;

namespace SwitchSupport.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
