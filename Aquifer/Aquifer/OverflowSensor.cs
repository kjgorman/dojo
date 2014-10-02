namespace Aquifer
{
    public class OverflowSensor
    {
        public bool Sense()
        {
            return SumpResevoir.Instance.Value.GetVolume();
        }
    }
}
