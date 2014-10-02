using System;

namespace Aquifer
{
    public class AirflowSensor
    {
        public bool Sense()
        {
            return new Random().NextDouble() < 0.5;
        }
    }
}
