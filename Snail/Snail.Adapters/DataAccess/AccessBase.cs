using NHibernate;

namespace Snail.Adapters.DataAccess
{
    public class AccessBase
    {
        public ISession Session = new SessionFactory()
                                        .CreateSessionFactory()
                                        .OpenSession();
    }
}
