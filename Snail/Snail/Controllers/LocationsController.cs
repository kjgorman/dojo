using System.Collections.Generic;
using System.Web.Http;
using Snail.Adapters.Locations;
using Snail.Core.Shipping.Routes;

namespace Snail.Controllers
{
    public class LocationsController : ApiController
    {
        public IEnumerable<Location> Get()
        {
            return new LocationProvider().All();
        }
    }
}
