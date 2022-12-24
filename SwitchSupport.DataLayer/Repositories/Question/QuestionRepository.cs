using Microsoft.EntityFrameworkCore;
using SwitchSupport.DataLayer.Context;
using SwitchSupport.Domain.Entities.Questions;
using SwitchSupport.Domain.Entities.Tags;
using SwitchSupport.Domain.Interfaces;
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
                .Select(t=>t.Tag.Title).ToListAsync();
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

       



        #endregion
    }
}
