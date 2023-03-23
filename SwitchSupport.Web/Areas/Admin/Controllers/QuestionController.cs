using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Question;

namespace SwitchSupport.Web.Areas.Admin.Controllers
{
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
    }
}
