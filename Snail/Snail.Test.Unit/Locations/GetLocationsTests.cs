using System.Linq;
using NUnit.Framework;
using Snail.Adapters.Model;
using Snail.Adapters.Providers;
using Snail.Controllers;
using Snail.Core.Shipping.Routes;
using Snail.Test.Machinery.Data;

namespace Snail.Test.Unit.Locations
{
    public class GetLocationsTests
    {
        [Test]
        public void SpecifyingNoConditionsWillRetrieveAllEntries()
        {
            var controller = LocationController();

            var results = controller.Get().ToList();

            Assert.That(results.Count(), Is.EqualTo(6));
            Assert.That(results.First().LocationId, Is.EqualTo(1L));
        }

        [Test]
        public void SpecifyingANonExistentCountryReturnsNoResults()
        {
            var controller = LocationController();

            var results = controller.Get("foo");

            Assert.That(results.Count(), Is.EqualTo(0));
        }

        [Test]
        public void SpecifyingAnExistingCountryWillReturnOnlyThatLocation()
        {
            var controller = LocationController();

            var results = controller.Get("Japan").ToList();

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().LocationId, Is.EqualTo(1L));
        }

        [Test]
        public void SpecifyingOnlyAPortNameIsAlsoPossible()
        {
            var controller = LocationController();

            var results = controller.Get(portName: "Malaga").ToList();

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().LocationId, Is.EqualTo(4L));
        }

        [Test]
        public void SpecifyingBothTheCountryAndPortWillReturnExactMatches()
        {
            var controller = LocationController();

            var results = controller.Get("England", "Liverpool").ToList();

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().LocationId, Is.EqualTo(6L));
        }

        private static LocationsController LocationController()
        {
            var locations = new[]
            {
                  new LocationModel {Id = 1L, CountryName = "Japan"  , PortName = "Tokyo"}
                , new LocationModel {Id = 2L, CountryName = "Korea"  , PortName = "Incheon"}
                , new LocationModel {Id = 3L, CountryName = "Italy"  , PortName = "Trieste"}
                , new LocationModel {Id = 4L, CountryName = "Spain"  , PortName = "Malaga"}
                , new LocationModel {Id = 5L, CountryName = "England", PortName = "Southampton"}
                , new LocationModel {Id = 6L, CountryName = "England", PortName = "Liverpool"}
            };

            var locationQuery = new MockQuery<LocationModel, Location>(locations,
                m => new Location(m.Id, m.CountryName, m.PortName));

            var locationProvider = new LocationProvider(locationQuery);

            return new LocationsController(locationProvider);
        }
    }
}
