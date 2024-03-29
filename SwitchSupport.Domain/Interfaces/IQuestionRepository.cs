﻿using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.Entities.Questions;
using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.ViewModels.Question;
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

        Task RemoveSelectTags(SelectQuestionTag selectQuestionTag);

        Task<Tag?> GetTagByName(string tagName);

        Task<bool> IsExistsTagByName(string name);

        Task<bool> CheckUserRequestTag(long userId, string tag);

        Task AddRequestTag(RequestTag requestTag);

        Task SaveChanges();

        Task<int> GetCountRequestTag(string tag);

        Task AddTag(Tag tag);

        Task<List<string>> GetTagsByQuestionId(long questionId);

        #endregion

        #region Question
        Task AddQuestion(Question question);

        Task UpdateQuestion(Question question);

        Task<Tag?> GetTagById(long id);

        Task AddQuestionTag(SelectQuestionTag questionTag);

        Task<IQueryable<Question>> GetAllQuestions();

        Task<Question?> GetQuestionById(long questionId);

        Task<bool> IsExistsUserScoreForQuestion(long questionId, long userId);

        Task AddQuestionUserScore(QuestionUserScore questionUserScore);

        Task<bool> IsExistsUserQuestionBookmarkByQuestinIdUserId(long questionId, long userId);

        Task<UserQuestionBookmark?> GetUserQuestionBookmarkByQuestinIdUserId(long questionId, long userId);

        void RemoveBookmark(UserQuestionBookmark questionBookmark);

        Task AddBookmark(UserQuestionBookmark questionBookmark);

        IQueryable<UserQuestionBookmark?> GetQuestionBookmark();
        #endregion

        #region Answer
        Task AnswerQuestion(Answer answerQuestion);

        Task<List<Answer>> GetQuestionAnswerList(long questionId);

        Task<Answer?> GetAnswerById(long answerId);

        Task UpdateAnswer(Answer answer);

        Task<bool> IsExistsUserScoreForScore(long userId,long answerId);

        Task AddAnswerUserScore(AnswerUserScore answerUserScore);
        #endregion



        #region View
        Task<bool> IsExistsViewForQuestion(string userIp,long questionId);

        Task AddQuestionView(QuestionView view);
        #endregion
    }
}
