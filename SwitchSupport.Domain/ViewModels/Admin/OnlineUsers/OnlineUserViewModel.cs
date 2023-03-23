using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Domain.ViewModels.Admin.OnlineUsers
{
    public class OnlineUserViewModel
    {
        public string UserId { get; set; }

        public string DisplayName { get; set; }    

        public string ConnectedDate { get; set;}
    }
}
