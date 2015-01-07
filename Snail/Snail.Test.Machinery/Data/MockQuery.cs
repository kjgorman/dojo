using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Snail.Adapters.DataAccess;

namespace Snail.Test.Machinery.Data
{
    public class MockQuery<TModel, T> : IBoxingQuery<TModel, T>
        where TModel : class
        where T      : class
    {
        private readonly IEnumerable<TModel> _underlyingCollection;
        private readonly Func<TModel, T> _boxingFunction;

        public MockQuery(IEnumerable<TModel> underlyingCollection
                       , Func<TModel, T> boxingFunction)
        {
            _underlyingCollection = underlyingCollection;
            _boxingFunction = boxingFunction;
        }

        public T Get<TKey>(TKey key)
        {
            // [!] this one needs some magic nhibernate metadata to determine the key propert(y|ies)
            //     i think
            throw new NotImplementedException();
        }

        public IEnumerable<T> All()
        {
            return _underlyingCollection.Select(_boxingFunction);
        }

        public IEnumerable<T> ByPredicate(Expression<Func<TModel, bool>> predicate)
        {
            return _underlyingCollection.Where(predicate.Compile()).Select(_boxingFunction);
        }
    }
}
