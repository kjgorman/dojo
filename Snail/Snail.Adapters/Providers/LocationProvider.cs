using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;
using Snail.Adapters.DataAccess;
using Snail.Adapters.Model;
using Snail.Core.Ports;
using Snail.Core.Shipping.Routes;

namespace Snail.Adapters.Providers
{
    public class LocationProvider : AccessBase, ILocationProvider
    {
        public IEnumerable<Location> All()
        {
            return Session
                .Query<LocationModel>()
                .Select(Map);
        }

        public Location ById(long locationId)
        {
            return Map(Session.Get<LocationModel>(locationId));
        }

        public IEnumerable<Location> ByCountry(string countryName)
        {
            return ByPredicate(CountryPredicate(countryName));
        }

        public IEnumerable<Location> ByPort(string portName)
        {
            return ByPredicate(PortPredicate(portName));
        }

        public IEnumerable<Location> ByExactLocation(string countryName, string portName)
        {
            return ByPredicate(
                x => CountryPredicate(countryName).Compile()(x) && PortPredicate(portName).Compile()(x));
        }

        private static Expression<Func<LocationModel, bool>> CountryPredicate(string countryName)
        {
            return x => x.CountryName == countryName;
        }

        private static Expression<Func<LocationModel, bool>> PortPredicate(string portName)
        {
            return x => x.PortName == portName;
        }

        private IEnumerable<Location> ByPredicate(Expression<Func<LocationModel, bool>> predicate)
        {
            return Session
                .QueryOver<LocationModel>()
                .Where(predicate)
                .Future<LocationModel>()
                .Select(Map);
        }

        private static Location Map(LocationModel model)
        {
            return new Location(model.Id, model.CountryName, model.PortName);
        }
    }
}
