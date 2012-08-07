using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace LapTimer.Infrastructure.Extensions
{
    public static class IListExtensions
    {
        /// <summary>
        /// Inplace Sort
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparison"></param>
        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            ArrayList.Adapter((IList)list).Sort(new ComparisonComparer<T>(comparison));
        }
    }
}