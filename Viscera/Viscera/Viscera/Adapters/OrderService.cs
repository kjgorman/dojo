using System;

namespace Viscera.Orders
{
    public class OrderService : Ports.Orders
    {
        public void Record(OrderStatistics order)
        {
            // N.B. you aren't allowed to just remove this...
            throw new AnnoyingIoException();
        }

        private class AnnoyingIoException : Exception
        {
            public override string Message
            {
                get
                {
                    return "Imagine this hit the database or a web service, or something else untenable for a unit test";
                }
            }
        }
    }
}
