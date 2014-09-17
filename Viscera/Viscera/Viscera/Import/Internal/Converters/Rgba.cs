using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Viscera.Paint;

namespace Viscera.Import.Internal.Converters
{
    internal class Rgba : Converter
    {
        protected override IEnumerable<Paint.Paint> Interpret(Stream inputStream)
        {
            var inputText = new StreamReader(inputStream).ReadToEnd(); //(10,255,0,0)

            var lines = inputText.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            return lines.Where(l => !string.IsNullOrWhiteSpace(l)).Select(PaintFromLine);
        }

        private Paint.Paint PaintFromLine(string line)
        {
            var components = line.Split(System.Convert.ToChar(","));
            var volume = Decimal.Parse(components.First());
            var channels = components.Skip(1);

            if (volume <= 0) throw new Exception("Volume must be greater than zero");
            return new Paint.Paint(volume, PigmentFromRgbChannels(channels.Select(int.Parse).ToArray()));
        }

        private Pigment PigmentFromRgbChannels(IList<int> channels)
        {
            if (channels.Any(c => c > 255)) throw new Exception("greater than 255");
            if (channels.Any(c => c < 0)) throw new Exception("less than 0");
            return new Pigment(Decimal.Divide(channels[0], 255), Decimal.Divide(channels[1], 255), Decimal.Divide(channels[2], 255), 0);
        }
    }
}
