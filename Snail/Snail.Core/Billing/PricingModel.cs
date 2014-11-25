using Snail.Core.Customer;

namespace Snail.Core.Billing
{
    public class PricingModel
    {
        private readonly Money _amount;
        private readonly CustomerAgreement _agreement;

        public PricingModel(Money amount, CustomerAgreement agreement)
        {
            _amount = amount;
            _agreement = agreement;
        }
    }
}
