using Ninject.Modules;
using Snail.Core.Billing;

namespace Snail.Modules
{
    public class CreationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIssuer>().To<Issuer>().InSingletonScope();
        }
    }
}