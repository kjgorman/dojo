using System.Linq;
using NUnit.Framework;
using Snail.Core.Shipping.Routes;
using Snail.Test.Machinery.Lang;
using Snail.Test.Machinery.Lang.Extensions;

namespace Snail.Test.Unit.Routes
{
    public class RouteSpecificationTests
    {
        [Test]
        public void Degenerate_SpecificationWithSameDestinationAsOriginIsSatisfiedByEmptyItinerary()
        {
            var location = Machinery.Lang.Locations.Somewhere();
            var route = new Specification(location, location);
            var itinerary = Itinerary.Empty();

            Assert.That(route.SatisfiedBy(itinerary));
        }

        [Test]
        public void Visitor_SpecificationWithAStepIsSatisfiedByAnItineraryWithALegBetweenThem()
        {
            var origin = Machinery.Lang.Locations.Somewhere();
            var destination = Machinery.Lang.Locations.Somewhere().SomewhereElse();

            var itinerary = new Itinerary(Enumerable.Empty<HandlingStep>()
                                        , Legs.Between(origin, destination));

            var route = new Specification(origin, destination);

            Assert.That(route.SatisfiedBy(itinerary));
        }
    }
}
