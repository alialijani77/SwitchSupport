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
        #endregion
    }
}
