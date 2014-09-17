using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Viscera.Orders;

namespace Viscera.Import.Internal
{
    internal class Importer : IImporter
    {
        private readonly Converter _converter;
        private readonly Ports.Orders _orders;

        internal Importer(Converter converter, Ports.Orders orders)
        {
            _converter = converter;
            _orders = orders;
        }

        public IEnumerable<Paint.Paint> Import(Stream stream)
        {
            var paints = _converter.Convert(stream).ToList();

            if (paints.Sum(p => p.VolumeInLitres) > 100)
            {
                throw new InvalidOperationException("Order quantity greater than stock on hand");
            }

            var numberRed = paints.Count(p => p.IsRed());
            var numberGreen = paints.Count(p => p.IsGreen());
            var numberBlue = paints.Count(p => p.IsBlue());

            _orders.Record(new OrderStatistics(numberRed, numberGreen, numberBlue));

            return paints;
        }
    }
}
