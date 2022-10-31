using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;

namespace SwitchSupport.Web.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly IQuestionService _questionService;

        #region ctor
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        #endregion
        #region Question
        [Authorize]
        [HttpGet("create-question")]
        public IActionResult CreateQuestion()
        {
            return View();
        }
        #endregion

        #region Tags
        [HttpGet("get-tags")]
        public async Task<IActionResult> GetTags(string name)
        {
            var tags = await _questionService.GetAllTags();
            var qu = tags.Where(t => t.Title.Contains(name)).Select(t=>t.Title).ToList();

            return Json(qu);
        }

        #endregion


    }
}
