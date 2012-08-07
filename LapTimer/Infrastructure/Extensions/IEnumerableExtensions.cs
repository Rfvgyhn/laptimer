using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LapTimer.Infrastructure.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, Comparison<T> comparison)
        {
            return list.OrderBy(t => t, new ComparisonComparer<T>(comparison));
        }
    }
}