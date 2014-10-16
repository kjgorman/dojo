using System.Collections.Generic;
using System.Linq;
using Ascensor.Internal;
using Ascensor.Machinery;

namespace Ascensor
{
    public class Elevator : DeterministicFiniteStateMachine<Elevator, ElevatorInput>
    {
        public enum Door { Open, Opening, Closing, Closed };
        public enum Movement { Up, Down, Idle };

        public uint     CurrentFloor;
        public uint?    RequestedFloor;
        public Door     Doors;
        public Movement Direction;

        public override HashSet<Elevator> InitialStates
        {
            // assume we also begin on the ground floor, with doors closed
            get { return new HashSet<Elevator> { Internal.States.Initial() }; }
        }

        public override HashSet<ElevatorInput> Inputs
        {
            // some inputs, e.g. a request from a floor, movement stop, door open timeout, door close
            get { return new HashSet<ElevatorInput>(Request.AllFloors
                                                   .Concat(Timeout.Timeouts)
                                                   .Concat(Internal.Movement.Movements)
                                                   .Concat(Arrival.Arrivals)); }
        }

        public override HashSet<Transition<Elevator, ElevatorInput>> Transitions
        {
            // possible transitions, e.g. an idle elevator with doors closed may receive a request input
            // and transition to a moving elevator in a given direction
            get { return Internal.Transitions.ElevatorTransition.All(); }
        }

        public override HashSet<Elevator> States
        {
            // possible states to be in
            get { return new HashSet<Elevator>(); }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Elevator) obj);
        }

        protected bool Equals(Elevator other)
        {
            return CurrentFloor   == other.CurrentFloor
                && RequestedFloor == other.RequestedFloor
                && Doors          == other.Doors
                && Direction      == other.Direction;
        }

        public Elevator With(uint? currentFloor = null
            , int? requestedFloor = -1
            , Door? door = null
            , Movement? direction = null)
        {
            return With(this, currentFloor, requestedFloor, door, direction);
        }

        public static Elevator With(Elevator state
            , uint? currentFloor = null
            , int? requestedFloor = -1
            , Door? door = null
            , Movement? direction = null)
        {
            return new Elevator
            {
                CurrentFloor = currentFloor ?? state.CurrentFloor,
                Direction = direction ?? state.Direction,
                Doors = door ?? state.Doors,
                RequestedFloor = requestedFloor == -1 ? state.RequestedFloor : (uint?)requestedFloor
            };
        }
    }
}
