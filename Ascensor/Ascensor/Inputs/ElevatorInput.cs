namespace Ascensor.Inputs
{
    public interface ElevatorInput
    {
    }

    public struct Request : ElevatorInput
    {
        public uint Floor;
    }
}
