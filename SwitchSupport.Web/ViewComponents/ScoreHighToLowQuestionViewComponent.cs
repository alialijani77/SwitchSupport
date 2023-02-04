using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Question;

namespace SwitchSupport.Web.ViewComponents
{
    public class ScoreHighToLowQuestionViewComponent : ViewComponent
    {
        private readonly IQuestionService _questionService;

        public ScoreHighToLowQuestionViewComponent(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var options = new FilterQuestionViewModel
            {
                TakeEntitiy = 5,
                Sort = FilterQuestionSortEnum.ScoreHighToLow
            };

            var result = await _questionService.GetAllQuestions(options);


            return View("ScoreHighToLowQuestion", result);

        }
    }
}
