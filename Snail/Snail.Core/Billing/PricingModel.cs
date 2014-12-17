namespace Snail.Core.Billing
{
    public class PricingModel
    {
        private readonly Money _amount;

        public PricingModel(Money amount)
        {
            _amount = amount;
        }
    }
}
