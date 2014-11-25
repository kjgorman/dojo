using System.Collections.Generic;
using Snail.Core.Lang.Validation.Exceptions;

namespace Snail.Core.Lang.Validation
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ValidateNonEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable.None())
                throw new ArgumentValidationException("Argument must be non-empty!");

            return enumerable;
        }
    }
}
