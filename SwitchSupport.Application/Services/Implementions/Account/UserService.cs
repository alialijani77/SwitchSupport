using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Generators;
using SwitchSupport.Application.Security;
using SwitchSupport.Application.Services.Implementions.SiteSettings;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Application.Statics;
using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.Enums;
using SwitchSupport.Domain.Interfaces;
using SwitchSupport.Domain.ViewModels.Account;
using SwitchSupport.Domain.ViewModels.Admin.User;
using SwitchSupport.Domain.ViewModels.Common;
using SwitchSupport.Domain.ViewModels.UserPanel.Account;
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
        private readonly ScoreManagementViewModel _socerManagment;


        #region ctor
        public UserService(IUserRepository userRepository, IEmailService emailService, IOptions<ScoreManagementViewModel> socerManagment)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _socerManagment = socerManagment.Value;
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

        #region ForgotPassword
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


        #endregion

        #region Reset-Password
        public async Task<User> GetUserByActivationCode(string activationcode)
        {
            return await _userRepository.GetUserByActivationcode(activationcode);
        }

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            var user = await _userRepository.GetUserByActivationcode(resetPassword.activationCode);
            if (user == null) return ResetPasswordResult.NotFound;

            var password = PasswordHelper.EncodePasswordMd5(resetPassword.Password);
            user.Password = password;
            user.IsEmailConfirmed = true;
            user.EmailActivationCode = CodeGenerator.CreateActivationCode();
            await _userRepository.UpdateUser(user);
            await _userRepository.Save();

            return ResetPasswordResult.Success;

        }
        #endregion

        #region User
        public async Task<User?> GetUserById(long userId)
        {
            return await _userRepository.GetUserById(userId);
        }


        public async Task<EditUserViewModel> GetEditUser(long userId)
        {
            var user = await _userRepository.GetUserById(userId);

            var edituser = new EditUserViewModel()
            {
                BirthDate = user.BirthDate != null ? user.BirthDate.Value.ToShamsi() : string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                CityId = user.CityId,
                CountryId = user.CountryId,
                Description = user.Description,
                PhoneNumber = user.PhoneNumber,
                GetNewsLetter = user.GetNewsLetter
            };
            return edituser;
        }

        public async Task<ResultEditInfo> EditUserInfo(EditUserViewModel editUser, long userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (!string.IsNullOrEmpty(editUser.BirthDate))
            {
                try
                {
                    var date = editUser.BirthDate.ToMiladi();
                    user.BirthDate = date;
                }
                catch (Exception)
                {
                    return ResultEditInfo.notvialid;
                }
            }
            user.FirstName = editUser.FirstName;
            user.LastName = editUser.LastName;
            user.PhoneNumber = editUser.PhoneNumber;
            user.CountryId = editUser.CountryId;
            user.CityId = editUser.CityId;
            user.Description = editUser.Description;
            user.GetNewsLetter = editUser.GetNewsLetter;

            await _userRepository.UpdateUser(user);
            await _userRepository.Save();

            return ResultEditInfo.success;
        }

        #endregion

        #region ChangePassword
        public async Task<ChangePasswordViewModel.ChangePasswordResult> ChangePassword(ChangePasswordViewModel changePassword, long userId)
        {
            var user = await _userRepository.GetUserById(userId);

            var password = PasswordHelper.EncodePasswordMd5(changePassword.OldPassword);

            if (user.Password != password)
            {
                return ChangePasswordViewModel.ChangePasswordResult.OldPasswordIsNotValid;
            }
            user.Password = PasswordHelper.EncodePasswordMd5(changePassword.NewPassword);
            await _userRepository.UpdateUser(user);
            await _userRepository.Save();

            return ChangePasswordViewModel.ChangePasswordResult.Success;
        }
        #endregion

        #region User Question
        public async Task UpdateUserScoreAndMedal(long userId, int score)
        {
            var user = await GetUserById(userId);
            if (user != null)
            {
                user.Score += score;
                await _userRepository.UpdateUser(user);
                await _userRepository.Save();

                if (user.Score >= _socerManagment.MinScoreForBronzeMedal && user.Score < _socerManagment.MinScoreForSilverMedal)
                {
                    if (user.Medal != null && user.Medal == UserMedal.Bronze) return;

                    user.Medal = UserMedal.Bronze;
                    await _userRepository.UpdateUser(user);
                    await _userRepository.Save();
                }
                else if (user.Score >= _socerManagment.MinScoreForSilverMedal && user.Score < _socerManagment.MinScoreForGoldMedal)
                {
                    if (user.Medal != null && user.Medal == UserMedal.Silver) return;

                    user.Medal = UserMedal.Silver;
                    await _userRepository.UpdateUser(user);
                    await _userRepository.Save();
                }
                else if (user.Score >= _socerManagment.MinScoreForGoldMedal)
                {
                    if (user.Medal != null && user.Medal == UserMedal.Gold) return;

                    user.Medal = UserMedal.Gold;
                    await _userRepository.UpdateUser(user);
                    await _userRepository.Save();
                }
            }
        }
        #endregion

        #region Admin-User

        public async Task<FilterUserAdminViewModel> GetFilterUserAdmin(FilterUserAdminViewModel filter)
        {
            var query = _userRepository.GetUserIQueryable();

            if (!string.IsNullOrEmpty(filter.UserSearch))
            {
                query = query.Where(u => (u.FirstName + "" + u.LastName).Trim().Contains(filter.UserSearch) || u.Email.Trim().Contains(filter.UserSearch));
            }
            switch (filter.AccountActivationStatus)
            {
                case AccountActivationStatus.All:
                    break;
                case AccountActivationStatus.Active:
                    query = query.Where(u => u.IsEmailConfirmed);
                    break;
                case AccountActivationStatus.Inactive:
                    query = query.Where(u => !u.IsEmailConfirmed);
                    break;
            }

            await filter.SetPaging(query);
            return filter;
        }

        public async Task<EditUserAdminViewModel?> GetEditUserAdmin(long userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return null;

            var result = new EditUserAdminViewModel();
            result.UserId = user.Id;
            result.GetNewsLetter = user.GetNewsLetter;
            result.PhoneNumber = user.PhoneNumber;
            result.IsEmailConfirmed = user.IsEmailConfirmed;
            result.Avatar = user.Avatar;
            result.Email = user.Email;
            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.CityId = user.CityId;
            result.CountryId = user.CountryId;
            result.BirthDate = user.BirthDate?.ToShamsi();
            result.Description = user.Description;
            result.IsAdmin = user.IsAdmin;
            result.IsBan = user.IsBan;
            result.Password = user.Password;

            return result;
        }

        public async Task<EditUserAdminResult> EditUserAdmin(EditUserAdminViewModel editUser)
        {
            var user = await _userRepository.GetUserById(editUser.UserId);
            if (user == null) return EditUserAdminResult.UserNotFound;

            if (!user.Email.Equals(editUser.Email) && await _userRepository.IsExistsUserByEmail(editUser.Email))
            {
                return EditUserAdminResult.NotValidEmail;
            }

            user.BirthDate = editUser.BirthDate?.ToMiladi();
            user.Description = editUser.Description;
            user.Avatar = editUser.Avatar;
            user.Email = editUser.Email;
            user.FirstName = editUser.FirstName;
            user.LastName = editUser.LastName;
            user.CityId = editUser.CityId;
            user.CountryId = editUser.CountryId;
            user.IsEmailConfirmed = editUser.IsEmailConfirmed;
            user.PhoneNumber = editUser.PhoneNumber;
            user.GetNewsLetter = editUser.GetNewsLetter;
            user.IsAdmin = editUser.IsAdmin;
            user.IsBan = editUser.IsBan;

            if(!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);

            }
            await _userRepository.UpdateUser(user);
            await _userRepository.Save();

            return EditUserAdminResult.Success;
        }

        #endregion
    }
}
