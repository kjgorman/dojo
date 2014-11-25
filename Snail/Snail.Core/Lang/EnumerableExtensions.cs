using System;
using System.Collections.Generic;
using System.Linq;

namespace Snail.Core.Lang
{
    public static class EnumerableExtensions
    {
        public static bool None<T>(this IEnumerable<T> enumerable)
        {
            return false == enumerable.Any();
        }

        public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
        {
            return false == enumerable.Any(pred);
        }
    }
}
