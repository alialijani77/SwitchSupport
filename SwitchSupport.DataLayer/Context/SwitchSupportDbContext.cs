using Microsoft.EntityFrameworkCore;
using SwitchSupport.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.DataLayer.Context
{
    public class SwitchSupportDbContext : DbContext
    {
        #region ctor
        public SwitchSupportDbContext(DbContextOptions<SwitchSupportDbContext> options) : base(options)
        {

        }
        #endregion

        #region DbSet
        public DbSet<User> Users { get; set; }
        #endregion
    }
}
