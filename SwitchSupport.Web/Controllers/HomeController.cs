using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SwitchSupport.Web.Controllers
{
    public class HomeController : BaseController
    {       
        public IActionResult Index()
        {
            return View();
        }
    }
}