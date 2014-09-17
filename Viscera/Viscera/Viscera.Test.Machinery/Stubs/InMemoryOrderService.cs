using System.Collections.Generic;
using Viscera.Orders;

namespace Viscera.Test.Machinery.Stubs
{
    public class InMemoryOrderService : Ports.Orders
    {
        readonly List<OrderStatistics> _storeList = new List<OrderStatistics>();

        public void Record(OrderStatistics order)
        {
            _storeList.Add(order);
        }
    }
}
