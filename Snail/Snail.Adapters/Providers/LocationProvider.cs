using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Snail.Adapters.DataAccess;
using Snail.Adapters.Model;
using Snail.Core.Ports;
using Snail.Core.Shipping.Routes;

namespace Snail.Adapters.Providers
{
    public class LocationProvider : ILocationProvider
    {
        private readonly IBoxingQuery<LocationModel, Location> _query;

        public LocationProvider(IBoxingQuery<LocationModel, Location> query)
        {
            _query = query;
        }

        public IEnumerable<Location> All()
        {
            return _query.All();
        }

        public Location ById(long locationId)
        {
            return _query.Get(locationId);
        }

        public IEnumerable<Location> ByCountry(string countryName)
        {
            return _query.ByPredicate(CountryPredicate(countryName));
        }

        public IEnumerable<Location> ByPort(string portName)
        {
            return _query.ByPredicate(PortPredicate(portName));
        }

        public IEnumerable<Location> ByExactLocation(string countryName, string portName)
        {
            return _query.ByPredicate(x => x.CountryName == countryName && x.PortName == portName);
        }

        private static Expression<Func<LocationModel, bool>> CountryPredicate(string countryName)
        {
            return x => x.CountryName == countryName;
        }

        private static Expression<Func<LocationModel, bool>> PortPredicate(string portName)
        {
            return x => x.PortName == portName;
        }

        private static Location Map(LocationModel model)
        {
            return new Location(model.Id, model.CountryName, model.PortName);
        }
    }
}
