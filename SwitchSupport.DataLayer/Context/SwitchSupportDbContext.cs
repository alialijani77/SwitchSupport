﻿using Microsoft.EntityFrameworkCore;
using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.Entities.Location;
using SwitchSupport.Domain.Entities.Questions;
using SwitchSupport.Domain.Entities.SiteSetting;
using SwitchSupport.Domain.Entities.Tags;
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

        public DbSet<State> States { get; set; }

        public DbSet<UserQuestionBookmark> UserQuestionBookmarks { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionView> QuestionViews { get; set; }

        public DbSet<SelectQuestionTag> SelectQuestionTags { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<RequestTag> RequestTags { get; set; }

        public DbSet<QuestionUserScore> QuestionUserScores { get; set; }

        public DbSet<AnswerUserScore> AnswerUserScores { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relations in modelBuilder.Model.GetEntityTypes().SelectMany(r=>r.GetForeignKeys()))
            {
                relations.DeleteBehavior = DeleteBehavior.Restrict;
            }

            #region Sead Data
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

            modelBuilder.Entity<User>().HasData(new User()
            {
                CreateDate = DateTime.Now,
                Email = "alialijani77@gmail.com",
                IsAdmin = true,
                Avatar = "DefaultAvatar.png",
                EmailActivationCode = Guid.NewGuid().ToString("N"),
                IsEmailConfirmed = true,
                Id = 1,
                Password = "20-2C-B9-62-AC-59-07-5B-96-4B-07-15-2D-23-4B-70" //123

            });

            modelBuilder.Entity<State>().HasData(new State()
            {
                CreateDate = DateTime.Now,
                Id = 1,
                ParentId = null,
                Title = "ایران"

            });
            modelBuilder.Entity<State>().HasData(new State()
            {
                CreateDate = DateTime.Now,
                Id = 2,
                ParentId = 1,
                Title = "تهران"

            });
            modelBuilder.Entity<State>().HasData(new State()
            {
                CreateDate = DateTime.Now,
                Id = 3,
                ParentId = 1,
                Title = "البرز"

            });
            modelBuilder.Entity<State>().HasData(new State()
            {
                CreateDate = DateTime.Now,
                Id = 4,
                ParentId = 1,
                Title = "اصفهان"

            });
            modelBuilder.Entity<Tag>().HasData(new Tag()
            {
                CreateDate = date,
                Id = 1,
                Title = "switch"

            });
            modelBuilder.Entity<Tag>().HasData(new Tag()
            {
                CreateDate = DateTime.Now,
                Id = 2,
                Title = "core"

            });
            modelBuilder.Entity<Tag>().HasData(new Tag()
            {
                CreateDate = DateTime.Now,
                Id = 3,
                Title = "card"

            });
            #endregion

            base.OnModelCreating(modelBuilder);
            
        }
    }
}
