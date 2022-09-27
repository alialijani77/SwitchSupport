using Microsoft.Extensions.DependencyInjection;
using SwitchSupport.Application.Services.Implementions.Account;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.DataLayer.Repositories.Account;
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

            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();

            #endregion
        }
    }
}
