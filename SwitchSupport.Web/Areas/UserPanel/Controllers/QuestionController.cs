using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.UserPanel.Question;

namespace SwitchSupport.Web.Areas.UserPanel.Controllers
{
    public class QuestionController : UserPanelBaseController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        #region QuestionBookMarks

        [HttpGet]
        public async Task<IActionResult> QuestionBookMarks(FilterQuestionBookMarksViewModel filter)
        {
            filter.userId = User.GetUserId();
            filter.TakeEntitiy = 1;
            var result = await _questionService.GetQuestionBookMarks(filter);

            return View(result);
        }

        #endregion
    }
}
