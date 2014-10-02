using System;

namespace Aquifer
{
    public class UnderflowSensor
    {
        public bool Sense()
        {
            return new Random().NextDouble() < 0.5;
        }
    }
}
