using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.ViewModels.Account;
using SwitchSupport.Domain.ViewModels.Admin.User;
using SwitchSupport.Domain.ViewModels.UserPanel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SwitchSupport.Domain.ViewModels.Account.ChangePasswordViewModel;

namespace SwitchSupport.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResult> RegisterUser(RegisterViewModel register);

        Task<LoginResult> CheckForLogin(LoginViewModel login);

        Task<User> GetUserByEmail(string email);

        Task<bool> EmailActivation(string activationcode);

        Task<ForgotPasswordResult> CheckForForgotPassword(ForgotPassword forgotPassword);

        Task<User> GetUserByActivationCode(string activationcode);

        Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel resetPassword);

        Task<User?> GetUserById(long userId);

        Task<EditUserViewModel> GetEditUser(long userId);

        Task<ResultEditInfo> EditUserInfo(EditUserViewModel editUser,long userId);

        Task<ChangePasswordResult> ChangePassword(ChangePasswordViewModel changePassword, long userId);

        Task UpdateUserScoreAndMedal(long userId,int score);

        Task<FilterUserAdminViewModel> GetFilterUserAdmin(FilterUserAdminViewModel filter);

        Task<EditUserAdminViewModel?> GetEditUserAdmin(long userId);

        Task<EditUserAdminResult> EditUserAdmin(EditUserAdminViewModel editUser);

        Task<bool> CheckUserPermission(long permissionId, long userId);
    }
}
