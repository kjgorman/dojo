namespace Snail.Core.Billing
{
    public class Money
    {
        private readonly Currency _currency;

        public Money(Currency currency)
        {
            _currency = currency;
        }
    }
}
