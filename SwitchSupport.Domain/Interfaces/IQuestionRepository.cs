using SwitchSupport.Domain.Entities.Questions;
using SwitchSupport.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.Interfaces
{
    public interface IQuestionRepository
    {
        #region Tags
        Task<List<Tag>> GetAllTags();

        Task<IQueryable<Tag>> GetAllFilterTags();

        Task UpdateTag(Tag tag);

        Task<Tag?> GetTagByName(string tagName);

        Task<bool> IsExistsTagByName(string name);

        Task<bool> CheckUserRequestTag(long userId, string tag);

        Task AddRequestTag(RequestTag requestTag);

        Task SaveChanges();

        Task<int> GetCountRequestTag(string tag);

        Task AddTag(Tag tag);

        #endregion

        #region Question
        Task AddQuestion(Question question);

        Task AddQuestionTag(SelectQuestionTag questionTag);

        Task<IQueryable<Question>> GetAllQuestions();

        #endregion
    }
}
