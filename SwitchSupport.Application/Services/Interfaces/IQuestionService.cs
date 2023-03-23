using SwitchSupport.Domain.Entities.Questions;
using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.Enums;
using SwitchSupport.Domain.ViewModels.Admin.Tag;
using SwitchSupport.Domain.ViewModels.Question;
using SwitchSupport.Domain.ViewModels.UserPanel.Question;
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

        Task<IQueryable<Question>> GetQuestion();
        Task<bool> AddQuestion(CreateQuestionViewModel createQuestion);

        Task<FilterQuestionViewModel> GetAllQuestions(FilterQuestionViewModel filter);

        Task<FilterTagViewModel> GetAllFilterTags(FilterTagViewModel filter);

        Task<Question?> GetQuestionById(long questionId);

        Task<QuestionScoreResult> CreateScoreForQuestion(long questionId, QustionScore type, long userId);

        Task<bool> CheckAddQuestionToBookmark(long questionId, long userId);

        Task<bool> IsExistsUserQuestionBookmarkByQuestinIdUserId(long questionId, long userId);

        Task<EditQuestionViewModel?> FillEditQuestionViewModel(long questionId, long userId);

        Task<bool> EditQuestion(EditQuestionViewModel editQuestion);

        Task<EditAnswerViewModel?> FillEditAnswerViewModel(long answerId, long userId);

        Task<bool> EditAnswer(EditAnswerViewModel editAnswer);

        Task<FilterQuestionBookMarksViewModel> GetQuestionBookMarks(FilterQuestionBookMarksViewModel filter);
        #endregion

        #region Answer

        Task<bool> AnswerQuestion(AnswerQuestionViewModel answerQuestion);

        Task<List<Answer>> GetQuestionAnswerList(long questionId);

        Task<bool> HasUserAccessToSelectTrueAnswer(long userId, long answerId);

        Task SelectTrueAnswer(long userId, long answerId);


        Task<AnswerScoreResult> CreateScoreForAnswer(long answerId, AnswerScore answerScore,long userId);      

        

        #endregion

        #region View
        Task AddViewForQuestion(string userIp, Question question);
        #endregion

        #region Admin

        Task<List<TagJsonViewModel>> GetTagListForChartJs();

        Task<FilterTagAdminViewModel> FilterTagAdmin(FilterTagAdminViewModel filter);

        Task CreateTagAdmin(CreateTagAdminViewModel createTag);

        Task<EditTagAdminViewModel> getTagForEditTagAdmin(long tagId);

        Task EditTagAdmin(EditTagAdminViewModel editTag);

        Task<bool> DeleteTag(long tagId);

        Task<bool> DeleteQuestion(long questionId);

        Task<bool> changeQuestionIsCheckedStatus(long questionId);


        #endregion
    }
}
