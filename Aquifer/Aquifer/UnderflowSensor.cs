namespace Aquifer
{
    public class UnderflowSensor
    {
        public bool Sense()
        {
            return SumpResevoir.Instance.Value.GetVolume();
        }
    }
}
