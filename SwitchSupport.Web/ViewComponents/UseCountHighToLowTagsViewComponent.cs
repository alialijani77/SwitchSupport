using Microsoft.AspNetCore.Mvc;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Question;

namespace SwitchSupport.Web.ViewComponents
{
    public class UseCountHighToLowTagsViewComponent : ViewComponent
    {
        private readonly IQuestionService _questionRepository;

        public UseCountHighToLowTagsViewComponent(IQuestionService questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var options = new FilterTagViewModel
            {
                Sort = FilterTagSortEnum.UseCountHighToLow,
                TakeEntitiy = 5
            };

            var result = await _questionRepository.GetAllFilterTags(options);

            return View("UseCountHighToLowTags", result);
        }

    }
}
