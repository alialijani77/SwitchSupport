using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Statics;
using System.Diagnostics;

namespace SwitchSupport.Web.Controllers
{
    public class HomeController : BaseController
    {       
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadCkeditor(IFormFile upload)
        {
            var filename = Guid.NewGuid() + Path.GetExtension(upload.FileName);
            upload.UploadFile(filename, PathTools.CkeditorServerPath);
            return Json(new { url = $"{PathTools.CkeditorPath}{filename}" });
        }
    }
}