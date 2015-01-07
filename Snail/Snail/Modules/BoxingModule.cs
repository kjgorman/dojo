using System;
using Ninject.Modules;
using Snail.Adapters.DataAccess;
using Snail.Adapters.Model;
using Snail.Core.Shipping.Routes;

namespace Snail.Modules
{
    public class BoxingModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IBoxingQuery<,>)).To(typeof(Query<,>));

            Bind<Func<LocationModel, Location>>()
                .ToMethod(_ => model => new Location(model.Id, model.CountryName, model.PortName));
        }
    }
}