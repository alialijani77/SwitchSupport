﻿using SwitchSupport.Application.Generators;
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

        #region Login
        public async Task<LoginResult> CheckForLogin(LoginViewModel login)
        {
            var user = await _userRepository.GetUserByEmail(login.Email.Trim().ToLower());

            if (user == null) return LoginResult.NoExists;

            var password = PasswordHelper.EncodePasswordMd5(login.Password);

            if (user.Password != password) return LoginResult.NoExists;

            if(user.IsBan) return LoginResult.IsBan;
            if (user.IsDelete) return LoginResult.IsDelete;
            if (!user.IsEmailConfirmed) return LoginResult.IsNotActive;

            return LoginResult.Success;
            
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email.Trim().ToLower());
        }
        #endregion
    }
}
