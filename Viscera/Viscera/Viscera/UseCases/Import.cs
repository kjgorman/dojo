using System.Collections.Generic;
using System.IO;
using Viscera.Import.Internal;
using Viscera.Orders;

namespace Viscera.UseCases
{
    public static class Import
    {
        public static IEnumerable<Paint.Paint> Rgba(Stream inputStream)
        {
            return new ImporterFactory().Create(ImportType.Rgba, inputStream).Import(inputStream);
        }

        public static IEnumerable<Paint.Paint> Cmyk(Stream inputStream)
        {
            return new ImporterFactory().Create(ImportType.Cmyk, inputStream).Import(inputStream);
        }
    }
}
