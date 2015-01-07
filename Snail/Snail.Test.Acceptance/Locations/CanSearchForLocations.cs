using System.Linq;
using Moq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NUnit.Framework;
using Snail.Adapters.Model;
using Snail.Adapters.Providers;
using Snail.Controllers;

namespace Snail.Test.Acceptance.Locations
{
    public class CanSearchForLocations
    {
        private LocationsController Locations()
        {
            var session = new Mock<ISession>();

            var locations = new[]
            {
                  new LocationModel { Id = 1L, CountryName = "japan", PortName = "tokyo" }
                , new LocationModel { Id = 1L, CountryName = "korea", PortName = "incheon" }
                , new LocationModel { Id = 1L, CountryName = "spain", PortName = "malaga" }
                , new LocationModel { Id = 1L, CountryName = "england", PortName = "southampton" }
            };

            session.Setup(s => s.QueryOver<LocationModel>()).Returns(() => locations);

            var provider = new LocationProvider(session.Object);

            return new LocationsController(provider);
        }

        [Test]
        public void GivenNoArgumentsWeGetAllTheResults()
        {
            var results = Locations().Get().ToList();

            Assert.That(results.Count(), Is.EqualTo(4));
            Assert.That(results.First().LocationId, Is.EqualTo(1L));
        }

    }
}
