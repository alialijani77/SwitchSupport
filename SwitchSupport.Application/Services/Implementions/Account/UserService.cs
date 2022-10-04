using SwitchSupport.Application.Generators;
using SwitchSupport.Application.Security;
using SwitchSupport.Application.Services.Implementions.SiteSettings;
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
        private readonly IEmailService _emailService;


        #region ctor
        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
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

            #region Send Activation Email

            var body = $@"
                <div> برای فعالسازی حساب کاربری خود روی لینک زیر کلیک کنید . </div>
                <a href='{PathTools.SiteAddress}/Activate-Email/{User.EmailActivationCode}'>فعالسازی حساب کاربری</a>";

            await _emailService.SendEmail(User.Email, "فعالسازی حساب کاربری", body);

            #endregion


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

            if (user.IsBan) return LoginResult.IsBan;
            if (user.IsDelete) return LoginResult.IsDelete;
            if (!user.IsEmailConfirmed) return LoginResult.IsNotActive;

            return LoginResult.Success;

        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email.Trim().ToLower());
        }


        #endregion

        #region EmailActivation
        public async Task<bool> EmailActivation(string activationcode)
        {
            var user = await _userRepository.GetUserByActivationcode(activationcode);

            if (user == null) return false;
            if (user.IsDelete || user.IsBan) return false;
            user.IsEmailConfirmed = true;
            user.EmailActivationCode = CodeGenerator.CreateActivationCode();
            await _userRepository.UpdateUser(user);
            await _userRepository.Save();
            return true;


        }
        #endregion

        public async Task<ForgotPasswordResult> CheckForForgotPassword(ForgotPassword forgotPassword)
        {
            var user = await _userRepository.GetUserByEmail(forgotPassword.Email.Trim().ToLower());

            if (user == null || user.IsDelete) return ForgotPasswordResult.NotFound;
            if (user.IsBan) return ForgotPasswordResult.IsBan;

            #region Send Activation Email

            var body = $@"
                <div> برای بازیابی رمز حساب کاربری خود روی لینک زیر کلیک کنید . </div>
                <a href='{PathTools.SiteAddress}/Reset-Password/{user.EmailActivationCode}'>بازیابی رمزحساب کاربری</a>";

            await _emailService.SendEmail(user.Email, "فعالسازی حساب کاربری", body);

            #endregion

            return ForgotPasswordResult.Success;
        }
    }
}
