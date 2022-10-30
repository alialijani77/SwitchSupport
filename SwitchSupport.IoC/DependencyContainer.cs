using Microsoft.Extensions.DependencyInjection;
using SwitchSupport.Application.Services.Implementions.Account;
using SwitchSupport.Application.Services.Implementions.Question;
using SwitchSupport.Application.Services.Implementions.SiteSettings;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.DataLayer.Repositories.Account;
using SwitchSupport.DataLayer.Repositories.Question;
using SwitchSupport.DataLayer.Repositories.SiteSetting;
using SwitchSupport.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IStateServices, StateServices>();
            services.AddScoped<IQuestionService, QuestionService>();




            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();

            #endregion
        }
    }
}
