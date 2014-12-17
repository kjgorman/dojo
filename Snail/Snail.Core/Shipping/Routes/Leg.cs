using System;

namespace Snail.Core.Shipping.Routes
{
    [Serializable]
    public class Leg
    {
        public readonly long     LegId;
        public readonly Location From;
        public readonly Location To;

        public Leg(Location @from, Location to, long legId)
        {
            // TODO – this should probably have an associated cost function?
            To    = to;
            From  = @from;
            LegId = legId;
        }
    }
}
