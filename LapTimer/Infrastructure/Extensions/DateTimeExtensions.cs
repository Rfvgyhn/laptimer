using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapTimer.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToSlug(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}