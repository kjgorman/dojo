using System;
using System.Collections.Generic;
using System.IO;

namespace Viscera.Import.Internal.Converters
{
    internal class Cmyk : Converter
    {
        protected override IEnumerable<Paint.Paint> Interpret(Stream inputStream)
        {
            throw new NotImplementedException();
        }

        /*
         *  For your convenience...
         * 
        private static Pigment CmykToPigment(int cyan, int magenta, int yellow, int key)
        {
            // this isn't about actual conversion...
            // h/t @harthur
            // https://github.com/harthur/color-convert/blob/7d613d6f3fed16266153ec2212dc5d877e3dd230/conversions.js#L366-L377

            const decimal alpha = 0;
            Func<int, decimal> toFraction = i => Decimal.Divide(i, 100);
            decimal c = toFraction(cyan)
                  , m = toFraction(magenta)
                  , y = toFraction(yellow)
                  , k = toFraction(key);

            Func<decimal, decimal> fromRgb = v => 1 - Math.Min(1, v * (1 - k) + k);
            var red = fromRgb(c);
            var green = fromRgb(m);
            var blue = fromRgb(y);

            return new Pigment(red, green, blue, alpha);
        }*/
    }
}
