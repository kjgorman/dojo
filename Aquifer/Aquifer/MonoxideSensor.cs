using System;

namespace Aquifer
{
    public class MonoxideSensor
    {
        public bool Sense()
        {
            return new Random().NextDouble() < 0.5;
        }
    }
}
