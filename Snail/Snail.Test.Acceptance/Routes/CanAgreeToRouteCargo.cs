using NUnit.Framework;

namespace Snail.Test.Acceptance.Routes
{
    public class CanAgreeToRouteCargo
    {
        [Test]
        public void CanAgreeWithACustomerToRouteSomeCargo()
        {
            Given_a_customer_who_can_pay_all_custom_fees();

            Shipping_some_cargo_from_athens_to_malaga();

            Should_produce_a_route_specification_with_no_customs_restrictions();
        }

        // TEST: should have an interrogable itinerary

        private void Should_produce_a_route_specification_with_no_customs_restrictions()
        {
            // TODO
        }

        private void Shipping_some_cargo_from_athens_to_malaga()
        {
            //TODO
        }

        private void Given_a_customer_who_can_pay_all_custom_fees()
        {
            // TODO
        }
    }
}
