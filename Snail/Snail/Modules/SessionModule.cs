using NHibernate;
using Ninject.Modules;
using Snail.Adapters.DataAccess;

namespace Snail.Modules
{
    public class SessionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISession>().ToMethod(ctx => new SessionFactory()
                .CreateSessionFactory()
                .OpenSession());
        }
    }
}