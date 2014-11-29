using NHibernate;

namespace Snail.Adapters.DataAccess
{
    public abstract class AccessBase
    {
        protected ISession Session = new SessionFactory()
                                        .CreateSessionFactory()
                                        .OpenSession();
    }
}
