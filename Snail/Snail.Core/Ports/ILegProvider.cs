using System.Collections.Generic;
using Snail.Core.Shipping.Routes;

namespace Snail.Core.Ports
{
    public interface ILegProvider
    {
        /// <summary>
        /// Returns all legs between two locations
        /// </summary>
        /// <returns></returns>
        IEnumerable<Leg> All();
    }
}
