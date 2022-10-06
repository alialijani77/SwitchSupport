using SwitchSupport.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Extensions
{
    public static class UserExtensions
    {
        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.SingleOrDefault(u => u.Type == ClaimTypes.NameIdentifier);
            if (userId == null) return 0;
            return long.Parse(userId.Value);
        }

        public static string GetUserDisplayName(this User user)
        {
            if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
            {
                return $"{user.FirstName} {user.LastName}";
            }
            return $"{user.Email.Split("@")[0]}";
        }
    }
}
