using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SwitchSupport.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminBaseController : Controller
    {
        public static readonly string SuccessMessage = "SuccessMessage";
        public static readonly string InfoMessage = "InfoMessage";
        public static readonly string WarningMessage = "WarningMessage";
        public static readonly string ErrorMessage = "ErrorMessage";
    }
}
