using System.Collections.Generic;
using System.Web.Http;
using Snail.Core.Ports;
using Snail.Core.Shipping.Routes;

namespace Snail.Controllers
{
    public class LegsController : ApiController
    {
        private readonly ILegProvider _provider;

        public LegsController(ILegProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public IEnumerable<Leg> Get()
        {
            return _provider.All();
        }
    }
}
