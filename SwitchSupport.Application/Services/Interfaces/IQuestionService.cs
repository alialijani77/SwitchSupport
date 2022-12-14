using SwitchSupport.Domain.Entities.Questions;
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

        Task<List<string>> GetTagsByQuestionId(long questionId);
        #endregion

        #region Quetion
        Task<bool> AddQuestion(CreateQuestionViewModel createQuestion);

        Task<FilterQuestionViewModel> GetAllQuestions(FilterQuestionViewModel filter);

        Task<FilterTagViewModel> GetAllFilterTags(FilterTagViewModel filter);

        Task<Question?> GetQuestionById(long questionId);

        #endregion

        #region Answer

        Task<bool> AnswerQuestion(AnswerQuestionViewModel answerQuestion);

        Task<List<Answer>> GetQuestionAnswerList(long questionId);

        Task<bool> HasUserAccessToSelectTrueAnswer(long userId, long answerId);

        Task SelectTrueAnswer(long userId, long answerId);

        #endregion

        #region View
        Task AddViewForQuestion(string userIp, Question question);
        #endregion
    }
}
