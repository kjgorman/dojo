using System.Collections.Generic;

namespace Ascensor.Machinery.Lang
{
    public class Enumerables
    {
        public static IEnumerable<T> Of<T>(T t)
        {
            return new[] { t };
        }
    }
}
