namespace Snail.Core.Shipping
{
    public class Cargo
    {
        private readonly long _cargoId;
        private readonly decimal _weight;
        private readonly HazMatCode _hazMatCode;

        public Cargo(long cargoId, decimal weight, HazMatCode hazMatCode)
        {
            _cargoId = cargoId;
            _weight = weight;
            _hazMatCode = hazMatCode;
        }
    }
}
