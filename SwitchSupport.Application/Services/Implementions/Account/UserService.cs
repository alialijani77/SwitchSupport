using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.Interfaces;
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
    }
}
