using System;

namespace Aquifer
{
    public class MethaneSensor
    {
        public bool Sense()
        {
            return new Random().NextDouble() < 0.5;
        }
    }
}
