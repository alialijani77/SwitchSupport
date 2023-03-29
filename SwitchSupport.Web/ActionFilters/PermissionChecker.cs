using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SwitchSupport.Application.Extensions;
using SwitchSupport.Application.Services.Interfaces;
using System;

namespace SwitchSupport.Web.ActionFilters
{
    public class PermissionChecker : Attribute, IAsyncAuthorizationFilter
    {
        private readonly long _permissionId;

        public PermissionChecker(long permissionId)
        {
            _permissionId = permissionId;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService))!;

            if (!await userService.CheckUserPermission(_permissionId, context.HttpContext.User.GetUserId()))
            {
                context.Result = new ForbidResult();
            }

        }
    }
}
