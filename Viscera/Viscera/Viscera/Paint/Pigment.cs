using System;
using System.Linq;

namespace Viscera.Paint
{
    public class Pigment
    {
        private readonly decimal _red;
        private readonly decimal _green;
        private readonly decimal _blue;
        private readonly decimal _alpha;

        internal Pigment(decimal red, decimal green, decimal blue, decimal alpha)
        {
            new [] {red, green, blue, alpha}.Any(ThrowIfOutsideRange(0, 1));

            _red = red;
            _green = green;
            _blue = blue;
            _alpha = alpha;
        }

        private static Func<decimal, bool> ThrowIfOutsideRange(decimal lo, decimal hi)
        {
            return d =>
            {
                if (d < lo || d > hi) throw new ArgumentOutOfRangeException("Pigment values must be in (0, 1)");

                return false;
            };
        }

        internal bool IsRed()
        {
            return _red > _green && _red > _blue;
        }

        internal bool IsBlue()
        {
            return _blue > _red && _blue > _green;
        }

        internal bool IsGreen()
        {
            return _green > _blue && _green > _red;
        }

        public override bool Equals(object other)
        {
            if (null == other) return false;
            if (false == (other is Pigment)) return false;
            var otherPigment = (Pigment) other;

            return otherPigment._red == _red
                   && otherPigment._blue == _blue
                   && otherPigment._green == _green
                   && otherPigment._alpha == _alpha;
        }

        public override int GetHashCode()
        {
            return _red.GetHashCode()
                   ^ _green.GetHashCode()
                   ^ _blue.GetHashCode()
                   ^ _alpha.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}, {3})", _red, _green, _blue, _alpha);
        }
    }
}
