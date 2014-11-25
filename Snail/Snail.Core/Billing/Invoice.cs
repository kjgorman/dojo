namespace Snail.Core.Billing
{
    public class Invoice
    {
        private readonly Customer.Customer _customer;
        private readonly Money _amount;

        public Invoice(Customer.Customer customer, Money amount)
        {
            _customer = customer;
            _amount = amount;
        }
    }
}
