using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace LapTimer.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string Pluralize(this string s)
        {
            PluralizationService plural = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
            return plural.Pluralize(s);
        }
    }
}