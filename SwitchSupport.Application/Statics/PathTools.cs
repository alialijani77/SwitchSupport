﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Statics
{
    public static class PathTools
    {
        #region User

        public static readonly string DefaultUserAvatar = "DefaultAvatar.png";

        public static readonly string UserAvatarServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwrroot/content/user/");
        public static readonly string UserAvatarPath = "/content/user/";

        public static readonly string SiteAddress = "https://localhost:7003";

        #endregion

        #region CKeditor

        public readonly static string CkeditorServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/ckeditor/");

        public readonly static string CkeditorPath = "/content/ckeditor/";


        #endregion
    }
}
