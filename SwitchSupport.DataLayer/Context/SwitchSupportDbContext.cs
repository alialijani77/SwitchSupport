using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
