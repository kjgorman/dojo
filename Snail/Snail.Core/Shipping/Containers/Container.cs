using System;
using System.Collections.Generic;

namespace Snail.Core.Shipping.Containers
{
    public class Container<T>
    {
        private readonly int _capacity;
        private readonly Stack<T> _contents;

        public Container(int capacity)
        {
            if (capacity <= 0) throw new ArgumentException("Capacity must be a positive non-zero value");

            _capacity = capacity;
            _contents = new Stack<T>(_capacity);
        }

        public virtual void Add(T t)
        {
            if (_contents.Count == _capacity)
                throw new InvalidOperationException("This container is already filled");

            _contents.Push(t);
        }

        public virtual T Get()
        {
            // the underlying stack will throw as well, but lets do it ourselves
            // to customize the message, and hide the actual stack implementation.
            if (_contents.Count == 0)
                throw new InvalidOperationException("Container is empty!");

            return _contents.Pop();
        }

        protected T Peek()
        {
            return _contents.Peek();
        }

        public decimal FillFactor()
        {
            return decimal.Divide(_contents.Count, _capacity);
        }
    }
}
