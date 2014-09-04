using System;
using System.Collections.Generic;
using System.IO;

namespace Viscera.Import.Internal
{
    internal abstract class Converter
    {
        internal IEnumerable<Paint.Paint> Convert(Stream stream)
        {
            throw new NotImplementedException();
        }

        protected abstract IEnumerable<Paint.Paint> Interpret(Stream stream);
    }
}
