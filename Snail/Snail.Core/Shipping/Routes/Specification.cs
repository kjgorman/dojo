using System.Collections.Generic;
using System.Linq;

namespace Snail.Core.Shipping.Routes
{
    public class Specification
    {
        private readonly Location _origin;
        private readonly Location _destination;
        private readonly HashSet<Location> _impassableCustoms;

        public Specification(Location origin
                           , Location destination
                           , IEnumerable<Location> impassableCustoms = null)
        {
            _origin = origin;
            _destination = destination;
            _impassableCustoms = new HashSet<Location>(impassableCustoms ?? Enumerable.Empty<Location>());
        }

        public bool SatisfiedBy(Itinerary itinerary)
        {
            if (_origin.Equals(_destination)) return true;

            return itinerary.Traverse(this);
        }

        public bool CanVisit(Location to)
        {
            return false == _impassableCustoms.Contains(to);
        }

        public bool EndsWith(Location to)
        {
            return _destination.Equals(to);
        }
    }
}
