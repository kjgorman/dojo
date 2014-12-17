using NUnit.Framework;

namespace Snail.Test.Acceptance.Routes
{
    public class CanDenyFreeloaders
    {
        [Test]
        public void CanRefuseToRouteCargoForACustomerWhoCantPayAnything()
        {
            Given_a_customer_who_cannot_pay_any_fees();

            Shipping_cargo_on_a_route_with_an_associated_cost();

            // or perhaps there's a concept that represents this use
            // case more accurately, perhaps a FailedNegotiation... or a Lowball
            Should_not_produce_a_specification(); 
        }

        private void Should_not_produce_a_specification()
        {
            throw new System.NotImplementedException();
        }

        private void Shipping_cargo_on_a_route_with_an_associated_cost()
        {
            throw new System.NotImplementedException();
        }

        private void Given_a_customer_who_cannot_pay_any_fees()
        {
            throw new System.NotImplementedException();
        }
    }
}
