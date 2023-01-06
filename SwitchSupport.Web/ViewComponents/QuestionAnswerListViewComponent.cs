using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;

namespace SwitchSupport.Web.ViewComponents
{
    public class QuestionAnswerListViewComponent : ViewComponent
    {
        private readonly IQuestionService _questionService;
        public QuestionAnswerListViewComponent(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long questionId)
        {
            return View("QuestionAnswerList",await _questionService.GetQuestionAnswerList(questionId));
        }
    }
}
