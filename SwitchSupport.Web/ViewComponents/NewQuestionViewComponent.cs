using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Question;

namespace SwitchSupport.Web.ViewComponents
{
    public class NewQuestionViewComponent : ViewComponent
    {
        private readonly IQuestionService _questionService;

        public NewQuestionViewComponent(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var options = new FilterQuestionViewModel
            {
                TakeEntitiy = 5,
                Sort = FilterQuestionSortEnum.NewToOld
            };

            var result = await _questionService.GetAllQuestions(options);


            return View("NewQuestion", result);
        }

    }
}
