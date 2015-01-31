using System;

namespace Snail.Core.Shipping.Containers
{
    public class BasicContainer : Container<Cargo>
    {
        public BasicContainer(int capacity) : base(capacity)
        {
        }

        public override void Add(Cargo t)
        {
            if (t.IsHazardous && FillFactor() != 0 && Peek().IsHazardous)
                throw new InvalidOperationException("Cannot store hazardous cargo adjacently");

            base.Add(t);
        }
    }
}
