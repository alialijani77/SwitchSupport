using Microsoft.EntityFrameworkCore;
using SwitchSupport.DataLayer.Context;
using SwitchSupport.Domain.Entities.SiteSetting;
using SwitchSupport.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.DataLayer.Repositories.SiteSetting
{
    public class SiteSettingRepository : ISiteSettingRepository
    {
        private readonly SwitchSupportDbContext _context;
        public SiteSettingRepository(SwitchSupportDbContext context)
        {
            _context = context;
        }
        public async Task<EmailSetting> GetDefaultEmail()
        {
            return await _context.EmailSettings.FirstOrDefaultAsync(e => e.IsDefault && !e.IsDelete);
        }
    }
}
