using Moq;
using Snail.Core.Billing;
using Snail.Core.Customer;
using Snail.Core.Ports;
using Snail.Core.Shipping;
using Snail.Core.Shipping.Routes;

namespace Snail.Test.Acceptance.Routes
{
    internal class RouteInteractor
    {
        private PathFinder _pathFinder;
        private Itinerary _itinerary;
        private Location _origin;
        private Location _destination;
        private Specification _specification;
        private CustomerAgreement _agreement;

        public void SetupLegsBetween(string originCountry
                                   , string originPort
                                   , string destinationCountry
                                   , string destinationPort)
        {
            _origin = new Location(1L, originCountry, originPort);
            _destination = new Location(2L, destinationCountry, destinationPort);
            _specification = new Specification(_origin, _destination);

            var intermediary = new Location(3L, "foo", "bar");

            var legProvider = new Mock<ILegProvider>();
            legProvider.Setup(l => l.All()).Returns(new []
            {
                new Leg(_origin, intermediary, 1L), 
                new Leg(intermediary, _destination, 2L)
            });

            _pathFinder = new PathFinder(legProvider.Object);
            _agreement = new CustomerAgreement(_specification, new PricingModel(Money.Unlimited));
        }

        public void BuildRoute()
        {
            _itinerary = _pathFinder.FindPathFor(_agreement);
        }

        public Itinerary GetRoute()
        {
            return _itinerary;
        }

        public Specification GetSpec()
        {
            return _specification;
        }
    }
}
