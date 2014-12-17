namespace Snail.Core.Shipping.Routes
{
    public class Specification
    {
        private readonly Location _origin;
        private readonly Location _destination;

        public Specification(Location origin
                           , Location destination)
        {
            _origin      = origin;
            _destination = destination;
        }

        public Location Origin { get { return _origin; }}

        public bool SatisfiedBy(Itinerary itinerary)
        {
            if (_origin.Equals(_destination)) return true;

            return itinerary.Traverse(this);
        }
        
        public bool EndsWith(Location to)
        {
            return _destination.Equals(to);
        }
    }
}
