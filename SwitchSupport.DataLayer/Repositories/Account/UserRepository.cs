using SwitchSupport.DataLayer.Context;
using SwitchSupport.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.DataLayer.Repositories.Account
{
    public class UserRepository : IUserRepository
    {
        private readonly SwitchSupportDbContext _context;

        #region ctor
        public UserRepository(SwitchSupportDbContext context)
        {
            _context = context;
        }
        #endregion
    }
}
