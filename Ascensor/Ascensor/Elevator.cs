using System;
using System.Collections.Generic;
using Ascensor.Inputs;
using Ascensor.Machinery;

namespace Ascensor
{
    public class Elevator : NonDeterministicFiniteStateMachine<Elevator, ElevatorInput>
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
            get { return new HashSet<ElevatorInput>(); }
        }

        public override HashSet<Func<Elevator, ElevatorInput, Elevator>> Transitions
        {
            // possible transitions, e.g. an idle elevator with doors closed may receive a request input
            // and transition to a moving elevator in a given direction
            get { return new HashSet<Func<Elevator, ElevatorInput, Elevator>>(); }
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
            return CurrentFloor  == other.CurrentFloor
                && RequestedFloor == other.RequestedFloor
                && Doors         == other.Doors
                && Direction     == other.Direction;
        }
    }
}
