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
