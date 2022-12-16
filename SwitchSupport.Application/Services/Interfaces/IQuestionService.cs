using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.ViewModels.Question;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Services.Interfaces
{
    public interface IQuestionService
    {
        #region Tags
        Task<List<Tag>> GetAllTags();

        Task<CreateQuestionResult> CheckTagValidation(long userId, List<string>? tags);
        #endregion

        #region Quetion
        Task<bool> AddQuestion(CreateQuestionViewModel createQuestion);

        Task<FilterQuestionViewModel> GetAllQuestions(FilterQuestionViewModel filter);

        Task<FilterTagViewModel> GetAllFilterTags(FilterTagViewModel filter);

        #endregion
    }
}
