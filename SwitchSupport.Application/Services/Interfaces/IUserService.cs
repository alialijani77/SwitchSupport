using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResult> RegisterUser(RegisterViewModel register);

        Task<LoginResult> CheckForLogin(LoginViewModel login);

        Task<User> GetUserByEmail(string email);

        Task<bool> EmailActivation(string activationcode);
    }
}
