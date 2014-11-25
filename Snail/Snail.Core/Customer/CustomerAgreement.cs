using Snail.Core.Shipping.Routes;

namespace Snail.Core.Customer
{
    public class CustomerAgreement
    {
        private readonly Specification _routeSpecification;

        public CustomerAgreement(Specification routeSpecification)
        {
            _routeSpecification = routeSpecification;
        }
    }
}
