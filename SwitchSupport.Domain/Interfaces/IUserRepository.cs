using SwitchSupport.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsExistsUserByEmail(string email);

        Task CreateUser(User user);

        Task Save();
    }
}
