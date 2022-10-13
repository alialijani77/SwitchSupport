using Microsoft.EntityFrameworkCore;
using SwitchSupport.DataLayer.Context;
using SwitchSupport.Domain.Entities.Location;
using SwitchSupport.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.DataLayer.Repositories.SiteSetting
{
    public class StateRepository : IStateRepository
    {
        private readonly SwitchSupportDbContext _context;

        public StateRepository(SwitchSupportDbContext context)
        {
            _context = context;
        }
        public async Task<List<State>> GetAllState(long? stateId = null)
        {
            var res = _context.States.Where(s => !s.IsDelete).AsQueryable();

            if (stateId.HasValue)
            {
                res = res.Where(s => s.ParentId.HasValue && s.ParentId.Value == stateId.Value);
            }
            else
            {
                res = res.Where(s => s.ParentId == null);
            }
            
            return await res.ToListAsync();
        }
    }
}
