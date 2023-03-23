using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Interfaces;
using SwitchSupport.Domain.ViewModels.Admin.OnlineUsers;

namespace SwitchSupport.Web.Hubs
{
    [Authorize]
    public class OnlineUsersHub : Hub
    {
        #region ctor

        private readonly IUserService _userService;

        public OnlineUsersHub(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        private readonly static Dictionary<long, OnlineUserViewModel> OnlineUsersList = new Dictionary<long, OnlineUserViewModel>();

        public async override Task OnConnectedAsync()
        {
            var userId = Context.User?.GetUserId();
            if (userId == null) return;

            if (OnlineUsersList.ContainsKey(userId.Value)) return;

            var user = await _userService.GetUserById(userId.Value);
            if (user == null) return;

            var OnlineUser = new OnlineUserViewModel()
            {
                ConnectedDate = $"{DateTime.Now.ToShamsi()} - {DateTime.Now:HH:mm:ss}",
                DisplayName = user.GetUserDisplayName(),
                UserId = userId.Value.ToString()
            };
            OnlineUsersList.Add(userId.Value, OnlineUser);

            await Clients.All.SendAsync("NewUserConnected", OnlineUser);


            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.GetUserId();
            if (userId == null) return;

            if (!OnlineUsersList.ContainsKey(userId.Value)) return;
         
            OnlineUsersList.Remove(userId.Value);

            await Clients.All.SendAsync("NewUserDisConnected", userId.Value);

			await base.OnDisconnectedAsync(exception);
        }

        public List<OnlineUserViewModel> GetAllConnectedUsers()
        {
            var users = OnlineUsersList.Values.ToList();

            return users;
        }


    }
}
