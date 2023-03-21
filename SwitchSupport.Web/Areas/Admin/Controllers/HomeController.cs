using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Admin.Tag;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Net.NetworkInformation;
using System;

namespace SwitchSupport.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IQuestionService _questionService;
        public HomeController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public async Task<IActionResult> Dashboard()
        {
            ViewData["ChartDataJson"] = JsonConvert.SerializeObject(await _questionService.GetTagListForChartJs());
            return View();
        }

        public async Task<IActionResult> LoadFilterTagsPartial(FilterTagAdminViewModel filter)
        {
            filter.TakeEntitiy = 10;
            var result = await _questionService.FilterTagAdmin(filter);
            return PartialView("_FilterTagsPartial", result);
        }

        public async Task<IActionResult> loadCreateTagModal()
        {
            return PartialView("_CreateTagAdminPartial");
        }
        [HttpPost]
        public async Task<IActionResult> CreateTag(CreateTagAdminViewModel createTag)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { status = "error", msg = "عملیات با موفقیت انجام نشد ." });
            }
            await _questionService.CreateTagAdmin(createTag);
            return new JsonResult(new { status = "success", msg = "عملیات با موفقیت انجام شد ." });

        }
        public async Task<IActionResult> loadEditTagModal(long tagId)
        {
            var tag = await _questionService.getTagForEditTagAdmin(tagId);
            if (tag == null)
            {
                return PartialView("_NotFoundDataPartial");

            }
            return PartialView("_EditTagAdminPartial", tag);
        }
        [HttpPost]
        public async Task<IActionResult> EditTag(EditTagAdminViewModel editTag)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { status = "error", msg = "عملیات با موفقیت انجام نشد ." });
            }
            await _questionService.EditTagAdmin(editTag);
            return new JsonResult(new { status = "success", msg = "عملیات با موفقیت انجام شد ." });
        }
    }
}

