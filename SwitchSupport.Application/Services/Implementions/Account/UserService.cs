using SwitchSupport.Application.Generators;
using SwitchSupport.Application.Security;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Application.Statics;
using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.Interfaces;
using SwitchSupport.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Services.Implementions.Account
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        #region ctor
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Register
        public async Task<RegisterResult> RegisterUser(RegisterViewModel register)
        {
            if (await _userRepository.IsExistsUserByEmail(register.Email.Trim().ToLower()))
            {
                return RegisterResult.EmailExists;
            }
            var Password = PasswordHelper.EncodePasswordMd5(register.Password);

            var User = new User();
            User.Email = register.Email.Trim().ToLower();
            User.Password = Password;
            User.EmailActivationCode = CodeGenerator.CreateActivationCode();
            User.Avatar = PathTools.DefaultUserAvatar;

            await _userRepository.CreateUser(User);
            await _userRepository.Save();

            return RegisterResult.Success;

        }
        #endregion
    }
}
