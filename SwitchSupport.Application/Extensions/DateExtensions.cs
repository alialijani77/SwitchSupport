using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchSupport.Application.Extensions
{
    public static class DateExtensions
    {
        public static string TimeAgo(this DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("about {0} minutes ago", timeSpan.Minutes) :
                    "about a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("about {0} hours ago", timeSpan.Hours) :
                    "about an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("about {0} days ago", timeSpan.Days) :
                    "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("about {0} months ago", timeSpan.Days / 30) :
                    "about a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("about {0} years ago", timeSpan.Days / 365) :
                    "about a year ago";
            }

            return result;
        }
        public static string ToShamsi(this DateTime dt)
        {
            PersianCalendar pr = new PersianCalendar();
            return $"{pr.GetYear(dt)}/{pr.GetMonth(dt).ToString("00")}/{pr.GetDayOfMonth(dt).ToString("00")}";
        }

        public static DateTime ToMiladi(this string date)
        {
            var splited = date.Split("/");

            var year = Convert.ToInt32(splited[0]);
            var month = Convert.ToInt32(splited[1]);
            var day = Convert.ToInt32(splited[2]);

            return new DateTime(year, month, day, new PersianCalendar());
        }
    }
}
