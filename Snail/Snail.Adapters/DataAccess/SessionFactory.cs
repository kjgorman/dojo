using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Driver;

namespace Snail.Adapters.DataAccess
{
    internal class SessionFactory
    {
        internal ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(
                    MsSqlConfiguration
                        .MsSql2008.Driver<Sql2008ClientDriver>()
                        .ConnectionString(c => c.FromConnectionStringWithKey("Snail.Connection"))
                        .FormatSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionFactory>())
                .BuildSessionFactory();
        }
    }
}
