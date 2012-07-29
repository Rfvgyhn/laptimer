using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

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

        public static string BaseConvert(this string number, int fromBase, int toBase)
        {
            var digits = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_~-.";
            var length = number.Length;
            var result = string.Empty;

            var nibbles = number.Select(c => digits.IndexOf(c)).ToList();
            int newlen;
            do
            {
                var value = 0;
                newlen = 0;

                for (var i = 0; i < length; ++i)
                {
                    value = value * fromBase + nibbles[i];
                    if (value >= toBase)
                    {
                        if (newlen == nibbles.Count)
                        {
                            nibbles.Add(0);
                        }
                        nibbles[newlen++] = value / toBase;
                        value %= toBase;
                    }
                    else if (newlen > 0)
                    {
                        if (newlen == nibbles.Count)
                        {
                            nibbles.Add(0);
                        }
                        nibbles[newlen++] = 0;
                    }
                }
                length = newlen;
                result = digits[value] + result; //
            }
            while (newlen != 0);

            return result;
        }
    }
}