using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LapTimer.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string Pluralize(this string s)
        {
            PluralizationService plural = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
            return plural.Pluralize(s);
        }

        // http://stackoverflow.com/a/2921135/182821
        public static string ToSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static string FormatWith(this string str, params string[] args)
        {
            return string.Format(str, args);
        }
    }
}