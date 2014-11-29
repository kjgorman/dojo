using System.Collections.Generic;
using System.Web.Http;
using Snail.Core.Ports;
using Snail.Core.Shipping.Routes;

namespace Snail.Controllers
{
    public class LocationsController : ApiController
    {
        private readonly ILocationProvider _provider;

        public LocationsController(ILocationProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public IEnumerable<Location> Get(string countryName = null, string portName = null)
        {
            if (countryName != null && portName != null)
            {
                return _provider.ByExactLocation(countryName, portName);
            }

            if (countryName != null)
            {
                return _provider.ByCountry(countryName);
            }

            if (portName != null)
            {
                return _provider.ByPort(portName);
            }

            return _provider.All();
        }
    }
}
