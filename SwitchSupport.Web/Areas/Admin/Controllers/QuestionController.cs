using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.ViewModels.Question;
using SwitchSupport.Web.ActionFilters;

namespace SwitchSupport.Web.Areas.Admin.Controllers
{
    [PermissionChecker(3)]
    public class QuestionController : AdminBaseController
    {
        #region ctor
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        #endregion
        public async Task<IActionResult> QuestionsList(FilterQuestionViewModel filter)
        {
            var result = await _questionService.GetAllQuestions(filter);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(long questionId)
        {
            var result = await _questionService.DeleteQuestion(questionId);
            if (result == false)
            {
                return new JsonResult(new { status = "error", msg = "عملیات با موفقیت انجام نشد ." });

            }
            return new JsonResult(new { status = "success", msg = "عملیات با موفقیت انجام شد ." });
        }
        [HttpPost]
        public async Task<IActionResult> changeQuestionIsCheckedStatus(long questionId)
        {
            var result = await _questionService.changeQuestionIsCheckedStatus(questionId);
            if (result == false)
            {
                return new JsonResult(new { status = "error", msg = "عملیات با موفقیت انجام نشد ." });

            }
            return new JsonResult(new { status = "success", msg = "عملیات با موفقیت انجام شد ." });
        }
    }
}
