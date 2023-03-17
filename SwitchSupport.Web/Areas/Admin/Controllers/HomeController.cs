using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Admin.Tag;

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
			filter.TakeEntitiy = 2;
			var result = await _questionService.FilterTagAdmin(filter);
			return PartialView("_FilterTagsPartial", result);
		}
	}
}
