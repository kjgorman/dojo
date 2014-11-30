using Ninject.Modules;
using Snail.Adapters.Providers;
using Snail.Core.Ports;

namespace Snail.Modules
{
    public class ProviderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationProvider>().To<LocationProvider>().InSingletonScope();
            Bind<ILegProvider>().To<LegProvider>().InSingletonScope();
        }
    }
}