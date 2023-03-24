using SwitchSupport.Domain.Entities.Account;
using SwitchSupport.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.ViewModels.Admin.User
{
    public class FilterUserAdminViewModel : Paging<SwitchSupport.Domain.Entities.Account.User>
    {
        public FilterUserAdminViewModel()
        {
            AccountActivationStatus = AccountActivationStatus.All;
        }

        public string? UserSearch { get; set; }

        public AccountActivationStatus AccountActivationStatus { get; set; }


    }

    public enum AccountActivationStatus
    {
        All,
        Active,
        Inactive,
    }
}
