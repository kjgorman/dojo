using Viscera.Orders;

namespace Viscera.Ports
{
    public interface Orders
    {
        void Record(OrderStatistics order);
    }
}