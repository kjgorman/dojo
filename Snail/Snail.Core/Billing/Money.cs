namespace Snail.Core.Billing
{
    public class Money
    {
        private readonly Currency _currency;

        public Money(Currency currency)
        {
            _currency = currency;
        }

        //TODO, where does the actual money come into it?
        public static Money Unlimited { get { return new Money(new Currency());} }
    }
}
