namespace Aquifer
{
    public class AirflowSensor
    {
        public bool Sense()
        {
            return SumpResevoir.Instance.Value.GetAirflowAsSignedDeviationFromMean() < -1;
        }
    }
}
