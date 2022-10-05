using Microsoft.EntityFrameworkCore;
using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.Entities.SiteSetting;
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

        public DbSet<EmailSetting> EmailSettings { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var date = DateTime.MinValue;

            modelBuilder.Entity<EmailSetting>().HasData(new EmailSetting()
            {
                CreateDate = date,
                DisplayName = "Switchsupport",
                EnableSSL = true,
                From = "swtsup80448@gmail.com",
                Id = 1,
                IsDefault = true,
                IsDelete = false,
                Password = "bhrghgjiayznrjiv",
                Port = 587,
                SMTP = "smtp.gmail.com"

            });
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
