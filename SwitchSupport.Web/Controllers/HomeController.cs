using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Application.Statics;
using SwitchSupport.Domain.ViewModels.Question;
using System.Diagnostics;

namespace SwitchSupport.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IQuestionService _questionService;

        public HomeController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IActionResult> Index()
        {
            var options = new FilterQuestionViewModel
            {
                TakeEntitiy = 10,
                Sort = FilterQuestionSortEnum.NewToOld
            };
            ViewData["Questions"] = await _questionService.GetAllQuestions(options);
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