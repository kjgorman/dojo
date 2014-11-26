using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Snail.Adapters.DataAccess;
using Snail.Core.Ports;
using Snail.Core.Shipping.Routes;

namespace Snail.Adapters.Locations
{
    public class LocationProvider : AccessBase, ILocationProvider
    {
        public IEnumerable<Location> All()
        {
            return Session.Query<Location>().ToList();
        }

        public Location ById(long locationId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Location> ByCity(string cityName)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Location> ByPort(string portName)
        {
            throw new System.NotImplementedException();
        }
    }
}
