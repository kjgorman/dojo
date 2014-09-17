using System.Text;

namespace Viscera.Paint
{
    public class Paint
    {
        public decimal VolumeInLitres { get; private set; }
        private readonly Pigment _colour;

        public Paint(decimal volumeInLitres, Pigment colour)
        {
            VolumeInLitres = volumeInLitres;
            _colour = colour;
        }

        public bool IsRed()
        {
            return _colour.IsRed();
        }

        public bool IsBlue()
        {
            return _colour.IsBlue();
        }

        public bool IsGreen()
        {
            return _colour.IsGreen();
        }

        private string DominantColour()
        {
            if (IsRed()) return "red";
            if (IsGreen()) return "green";
            if (IsBlue()) return "blue";

            return "grey";
        }

        public string ReceiptLine()
        {
            var sb = new StringBuilder();

            sb.Append(VolumeInLitres + " litres ");
            sb.Append("of " + DominantColour() + " paint");

            return sb.ToString();
        }

        public override bool Equals(object other)
        {
            if (null == other) return false;
            if (false == (other is Paint)) return false;
            var otherPaint = (Paint) other;

            return otherPaint.VolumeInLitres == VolumeInLitres
                   && otherPaint._colour.Equals(_colour);
        }

        public override int GetHashCode()
        {
            return VolumeInLitres.GetHashCode() ^ _colour.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[Paint]: {0}L\t{1}", VolumeInLitres, _colour.ToString());
        }
    }
}
