using System.IO;
using System.Text;

namespace Viscera.Test.Machinery.Extensions
{
    public static class Strings
    {
        public static Stream AsStream(this string whatever)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(whatever ?? string.Empty));
        }
    }
}
