using System.Collections.Generic;

namespace Viscera.Lang.Strings
{
    internal static class Extensions
    {
        internal static IEnumerable<string> Lines(this string self)
        {
            return self.Split('\n');
        }
    }
}
