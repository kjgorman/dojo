namespace Aquifer
{
    public class OverflowSensor
    {
        public bool Sense()
        {
            return new Random().NextDouble() < 0.5;
        }
    }
}
