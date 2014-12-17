using System.Collections.Generic;
using System.Linq;
using Snail.Core.Customer;
using Snail.Core.Lang;
using Snail.Core.Ports;
using Snail.Core.Shipping.Routes;

namespace Snail.Core.Shipping
{
    public class PathFinder
    {
        private readonly IEnumerable<Leg> _allLegs; 

        public PathFinder(ILegProvider legProvider)
        {
            _allLegs = legProvider.All();
        }

        public Itinerary FindPathFor(CustomerAgreement agreement)
        {
            var spec = agreement.Specification;
            var legs = FindLegs(spec.Origin, spec, Enumerable.Empty<Leg>());

            return new Itinerary(Enumerable.Empty<HandlingStep>(), legs);
        }

        private IEnumerable<Leg> FindLegs(Location currentLocation, Specification spec, IEnumerable<Leg> accumulator)
        {
            if (spec.EndsWith(currentLocation))
                return accumulator;

            var next = LegsFrom(currentLocation).ToList();

            if (next.None())
                return null;

            // note: I have no idea if this works properly
            return next
                .Select(l => FindLegs(l.To, spec, Cons(l, accumulator)))
                .Where(ls => ls != null)
                .OrderBy(ls => ls.Count())
                .First(); //or default?
        }

        // O(1) prepend innit
        private static IEnumerable<T> Cons<T>(T elem, IEnumerable<T> list)
        {
            yield return elem;
            foreach (var e in list) yield return e;
        }

        private IEnumerable<Leg> LegsFrom(Location location)
        {
            return _allLegs.Where(l => l.From.Equals(location));
        }
    }
}
