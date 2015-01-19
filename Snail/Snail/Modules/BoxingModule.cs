using System;
using AutoMapper;
using Ninject.Modules;
using Snail.Adapters.DataAccess;
using Snail.Adapters.Model;
using Snail.Core.Billing.Documents;
using Snail.Core.Shipping.Routes;

namespace Snail.Modules
{
    public class BoxingModule : NinjectModule
    {
        public override void Load()
        {
            RegisterMaps();

            Bind(typeof(IBoxingQuery<,>)).To(typeof(Query<,>));

            Bind<Func<LocationModel, Location>>()
                .ToMethod(_ => model => new Location(model.Id, model.CountryName, model.PortName));

            Bind<Func<DocumentModel, Quote>>()
                .ToMethod(_ => model => Mapper.Map<Quote>(model));
        }

        private static void RegisterMaps()
        {
            Mapper.CreateMap<DocumentModel, Quote>();
        }
    }
}