using System.Collections.Generic;
using System.IO;

namespace Viscera.Import.Internal
{
    internal interface IImporter
    {
        IEnumerable<Paint.Paint> Import(Stream stream);
    }
}
