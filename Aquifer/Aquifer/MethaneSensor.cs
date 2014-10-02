namespace Aquifer
{
    public class MethaneSensor
    {
        public bool Sense()
        {
            return SumpResevoir.Instance.Value.GetMethaneLevelAsPercentage() > 50;
        }
    }
}
