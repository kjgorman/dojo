using System.IO;
using Viscera.Import.Internal.Converters;

namespace Viscera.Import.Internal
{
    internal class ImporterFactory
    {
        internal IImporter Create(ImportType type, Stream inputStream)
        {
            return new Importer(new Rgba());
        }
    }
}
