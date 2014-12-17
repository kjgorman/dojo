using NUnit.Framework;

namespace Snail.Test.Acceptance.Routes
{
    public class CanAgreeToRouteCargo
    {
        private readonly RouteInteractor _interactor = new RouteInteractor();

        [Test]
        public void CanAgreeWithACustomerToRouteSomeCargo()
        {
            Given_a_customer_who_can_pay_all_custom_fees_on_legs_between_athens_and_malaga();

            Shipping_some_cargo_between_the_two();

            Should_produce_an_itinerary_directly_between_the_two();
        }

        // TEST: can figure out custom restrictions
        // TEST: can figure out cost thresholds

        private void Given_a_customer_who_can_pay_all_custom_fees_on_legs_between_athens_and_malaga()
        {
            _interactor.SetupLegsBetween("Greece", "Athens", "Spain", "Malaga");
        }

        private void Shipping_some_cargo_between_the_two()
        {
            _interactor.BuildRoute();
        }

        private void Should_produce_an_itinerary_directly_between_the_two()
        {
            var actual = _interactor.GetRoute();
            var spec   = _interactor.GetSpec();

            Assert.That(actual, Is.Not.Null);
            Assert.That(spec.SatisfiedBy(actual), Is.True);
        }
    }
}
