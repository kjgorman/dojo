namespace Ascensor.Internal
{
    /// <summary>
    /// A helper class to statically define elevator states.
    /// </summary>
    internal class States
    {
        internal static Elevator Initial()
        {
            return new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = null,
                Doors = Elevator.Door.Closed,
                Direction = Elevator.Movement.Idle
            };
        }
    }
}
