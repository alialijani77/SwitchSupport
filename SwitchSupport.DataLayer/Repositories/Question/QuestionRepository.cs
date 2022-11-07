using Microsoft.EntityFrameworkCore;
using SwitchSupport.DataLayer.Context;
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
        #endregion
    }
}
