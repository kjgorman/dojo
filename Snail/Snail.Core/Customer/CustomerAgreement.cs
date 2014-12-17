using Snail.Core.Billing;
using Snail.Core.Shipping.Routes;

namespace Snail.Core.Customer
{
    public class CustomerAgreement
    {
        private readonly PricingModel _pricing;
        public Specification Specification { get; private set; }

        public CustomerAgreement(Specification routeSpecification, PricingModel pricing)
        {
            _pricing = pricing;
            Specification = routeSpecification;
        }
    }
}
