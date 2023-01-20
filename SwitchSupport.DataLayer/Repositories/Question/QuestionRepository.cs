using Microsoft.EntityFrameworkCore;
using SwitchSupport.DataLayer.Context;
using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.Entities.Questions;
using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.Interfaces;
using SwitchSupport.Domain.ViewModels.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.DataLayer.Repositories.Question
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly SwitchSupportDbContext _context;

        #region ctor
        public QuestionRepository(SwitchSupportDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Tags
        public async Task<List<Tag>> GetAllTags()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<IQueryable<Tag>> GetAllFilterTags()
        {
            return _context.Tags.Where(t => !t.IsDelete).AsQueryable();
        }

        public async Task UpdateTag(Tag tag)
        {
            _context.Tags.Update(tag);
        }

        public async Task<bool> IsExistsTagByName(string name)
        {
            return await _context.Tags.AnyAsync(t => t.Title.Equals(name) && !t.IsDelete);
        }

        public async Task<bool> CheckUserRequestTag(long userId, string tag)
        {
            return await _context.RequestTags.AnyAsync(r => r.UserId == userId && !r.IsDelete && r.Title.Equals(tag));
        }

        public async Task AddRequestTag(RequestTag requestTag)
        {
            await _context.RequestTags.AddAsync(requestTag);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountRequestTag(string tag)
        {
            return await _context.RequestTags.CountAsync(r => r.Title.Equals(tag) && !r.IsDelete);
        }

        public async Task<List<string>> GetTagsByQuestionId(long questionId)
        {
            return await _context.SelectQuestionTags
                .Include(t => t.Tag)
                .Where(t => t.QuestionId == questionId)
                .Select(t => t.Tag.Title).ToListAsync();
        }

        public async Task AddTag(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
        }
        public async Task<Tag?> GetTagByName(string tagName)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => !t.IsDelete && t.Title.Equals(tagName));
        }
        #endregion

        #region Question
        public async Task AddQuestion(Domain.Entities.Questions.Question question)
        {
            await _context.Questions.AddAsync(question);
        }

        public async Task AddQuestionTag(SelectQuestionTag questionTag)
        {
            await _context.SelectQuestionTags.AddAsync(questionTag);
        }

        public async Task<IQueryable<Domain.Entities.Questions.Question>> GetAllQuestions()
        {
            //var res = _context.Questions.Where(q => !q.IsDelete).ToList();
            return _context.Questions.Where(q => !q.IsDelete).AsQueryable();
        }



        public async Task<Domain.Entities.Questions.Question?> GetQuestionById(long questionId)
        {
            return await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId && !q.IsDelete);
        }

        public async Task UpdateQuestion(Domain.Entities.Questions.Question question)
        {
            _context.Questions.Update(question);
        }

        public async Task<bool> IsExistsUserScoreForQuestion(long questionId, long userId)
        {
            return await _context.QuestionUserScores.AnyAsync(q => q.UserId == userId && q.QuestionId == questionId);
        }

        public async Task AddQuestionUserScore(QuestionUserScore questionUserScore)
        {
            await _context.QuestionUserScores.AddAsync(questionUserScore);
        }

        public async Task<bool> IsExistsUserQuestionBookmarkByQuestinIdUserId(long questionId, long userId)
        {
            return await _context.UserQuestionBookmarks.AnyAsync(u => u.UserId == userId && u.QuestionId == questionId);
        }

        public async Task<UserQuestionBookmark?> GetUserQuestionBookmarkByQuestinIdUserId(long questionId, long userId)
        {
            return await _context.UserQuestionBookmarks
                .FirstOrDefaultAsync(u => u.UserId == userId && u.QuestionId == questionId);
        }

        public void RemoveBookmark(UserQuestionBookmark questionBookmark)
        {
            _context.UserQuestionBookmarks.Remove(questionBookmark);
        }

        public async Task AddBookmark(UserQuestionBookmark questionBookmark)
        {
            await _context.UserQuestionBookmarks.AddAsync(questionBookmark);
        }

        #endregion

        #region Answer
        public async Task AnswerQuestion(Answer answerQuestion)
        {
            await _context.Answers.AddAsync(answerQuestion);
        }

        public async Task<List<Answer>> GetQuestionAnswerList(long questionId)
        {
            return await _context.Answers.Include(a => a.User)
                .Where(a => a.QuestionId == questionId && !a.IsDelete)
                .OrderByDescending(a => a.CreateDate).ToListAsync();
        }


        public async Task<Answer?> GetAnswerById(long answerId)
        {
            return await _context.Answers.Include(a => a.Question).FirstOrDefaultAsync(a => a.Id == answerId);
        }


        public async Task UpdateAnswer(Answer answer)
        {
            _context.Answers.Update(answer);
        }

        public async Task<bool> IsExistsUserScoreForScore(long userId, long answerId)
        {
            return await _context.AnswerUserScores.AnyAsync(a => a.UserId == userId && a.AnswerId == answerId);
        }

        public async Task AddAnswerUserScore(AnswerUserScore answerUserScore)
        {
            await _context.AnswerUserScores.AddAsync(answerUserScore);
        }


        #endregion

        #region View

        public async Task<bool> IsExistsViewForQuestion(string userIp, long questionId)
        {
            return await _context.QuestionViews.AnyAsync(q => q.UserIP.Equals(userIp) && q.QuestionId == questionId);
        }


        public async Task AddQuestionView(QuestionView view)
        {
            await _context.QuestionViews.AddAsync(view);
        }

        #endregion
    }
}
