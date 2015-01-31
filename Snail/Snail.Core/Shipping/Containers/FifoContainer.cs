using System.Collections.Generic;

namespace Snail.Core.Shipping.Containers
{
    public class FifoContainer : BasicContainer
    {
        private readonly Queue<Cargo> _queue = new Queue<Cargo>(); 

        public FifoContainer(int capacity) : base(capacity)
        {
        }

        public override void Add(Cargo t)
        {
            _queue.Enqueue(t);
            base.Add(t);
        }

        public override Cargo Get()
        {
            return _queue.Dequeue();
        }
    }
}
