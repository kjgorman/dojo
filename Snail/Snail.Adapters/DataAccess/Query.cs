using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;

namespace Snail.Adapters.DataAccess
{
    public class Query<TModel, T> : AccessBase, IBoxingQuery<TModel, T>
        where TModel : class
        where T      : class
    {
        private readonly Func<TModel, T> _map;

        public Query(Func<TModel, T> map)
        {
            _map = map;
        }

        public T Get<TKey>(TKey key)
        {
            return _map(Session.Get<TModel>(key));
        }

        public IEnumerable<T> All()
        {
            return Session.Query<TModel>().Select(_map);
        }

        public IEnumerable<T> ByPredicate(Expression<Func<TModel, bool>> predicate)
        {
            return Session
                .QueryOver<TModel>()
                .Where(predicate)
                .Future<TModel>()
                .Select(_map);
        }
    }

    public interface IBoxingQuery<TModel, out T>
        where TModel : class
        where T      : class
    {
        T Get<TKey>(TKey key);
        IEnumerable<T> All();
        IEnumerable<T> ByPredicate(Expression<Func<TModel, bool>> predicate);
    }
}
