namespace Aquifer
{
    public class MonoxideSensor
    {
        public bool Sense()
        {
            return SumpResevoir.Instance.Value.GetMonoxideLevelAsPercentage() > 50;
        }
    }
}
