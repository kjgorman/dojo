using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Snail.Adapters.DataAccess;
using Snail.Adapters.Model;
using Snail.Core.Ports;
using Snail.Core.Shipping.Routes;

namespace Snail.Adapters.Providers
{
    public class LegProvider : AccessBase, ILegProvider
    {
        public IEnumerable<Leg> All()
        {
            return Session
                .Query<LegModel>()
                .Select(l => new Leg(Map(l.From), Map(l.To), l.Id));
        }

        // TODO: duplication
        private static Location Map(LocationModel model)
        {
            return new Location(model.Id, model.CountryName, model.PortName);
        }
    }
}
