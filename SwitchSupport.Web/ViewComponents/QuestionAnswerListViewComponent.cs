using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Implementions.Question;

namespace SwitchSupport.Web.ViewComponents
{
    public class QuestionAnswerListViewComponent : ViewComponent
    {
        private readonly QuestionService _questionService;
        public QuestionAnswerListViewComponent(QuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long questionId)
        {
            return View("QuestionAnswerList",_questionService.GetQuestionAnswerList(questionId));
        }
    }
}
