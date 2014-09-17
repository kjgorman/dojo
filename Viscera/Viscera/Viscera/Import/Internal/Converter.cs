using System.Collections.Generic;
using System.IO;

namespace Viscera.Import.Internal
{
    internal abstract class Converter
    {
        internal IEnumerable<Paint.Paint> Convert(Stream stream)
        {
            return Interpret(stream);
        }

        protected abstract IEnumerable<Paint.Paint> Interpret(Stream stream);
    }
}
