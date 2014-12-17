using System.Collections.Generic;
using System.Linq;

namespace Snail.Core.Shipping.Routes
{
    public class Itinerary
    {
        private readonly Queue<Leg> _legs;
        private readonly Queue<HandlingStep> _handlingSteps;

        public Itinerary(IEnumerable<HandlingStep> handlingSteps
                       , IEnumerable<Leg> legs)
        {
            _handlingSteps = new Queue<HandlingStep>(handlingSteps);
            _legs = new Queue<Leg>(legs);
        }

        public static Itinerary Empty()
        {
            return new Itinerary(Enumerable.Empty<HandlingStep>(), Enumerable.Empty<Leg>());
        }

        public bool Traverse(Specification specification)
        {
            var steps = new Queue<Leg>(_legs);

            while (steps.Count > 0)
            {
                var next = steps.Dequeue();

                if (specification.EndsWith(next.To)) return true;
            }

            return false;
        }
    }
}
