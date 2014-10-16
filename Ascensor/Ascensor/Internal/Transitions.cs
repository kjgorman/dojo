using System;
using System.Collections.Generic;
using Ascensor.Machinery;

namespace Ascensor.Internal
{
    internal class Transitions
    {
        internal abstract class ElevatorTransition : Transition<Elevator, ElevatorInput>
        {
            internal static HashSet<Transition<Elevator, ElevatorInput>> All()
            {
                return new HashSet<Transition<Elevator, ElevatorInput>>(new ElevatorTransition[]
                {
                    new Request(), new Timeout(), new Movement(), new Arrival()
                });
            }
        }

        internal class Request : ElevatorTransition
        {
            public override bool Applicable(Elevator state, ElevatorInput input)
            {
                return input is Internal.Request && state.RequestedFloor == null;
            }

            public override Elevator Traverse(Elevator state, ElevatorInput input)
            {
                var request = (Internal.Request) input;

                // if we're moving, try to idle
                // if we're closed, start to open
                // if we're open, start closing
                // if we're idle, with a request?

                return new Elevator
                {
                    CurrentFloor = state.CurrentFloor,
                    Direction = state.Direction,
                    Doors = Elevator.Door.Opening,
                    RequestedFloor = request.Floor
                };
            }
        }

        internal class Arrival : ElevatorTransition
        {
            public override bool Applicable(Elevator state, ElevatorInput input)
            {
                return input is Internal.Arrival
                       && state.RequestedFloor != null
                       && state.CurrentFloor == state.RequestedFloor
                       && state.Doors == Elevator.Door.Closed;
            }

            public override Elevator Traverse(Elevator state, ElevatorInput input)
            {
                return state.With(requestedFloor: null, direction: Elevator.Movement.Idle);
            }
        }

        internal class Movement : ElevatorTransition
        {
            public override bool Applicable(Elevator state, ElevatorInput input)
            {
                return input is Internal.Movement 
                    && state.RequestedFloor != null;
            }

            private static int Step(Elevator state)
            {
                switch (state.Direction)
                {
                    case Elevator.Movement.Up  : return 1; // TODO make sure we can move off the top of the building...
                    case Elevator.Movement.Idle: return 0;
                    case Elevator.Movement.Down:
                        return state.CurrentFloor > 0 ? -1 : 0;
                }

                throw new ArgumentOutOfRangeException();
            }

            public override Elevator Traverse(Elevator state, ElevatorInput input)
            {
                var direction = state.RequestedFloor > state.CurrentFloor
                    ? Elevator.Movement.Up
                    : Elevator.Movement.Down;

                var current = (int) state.CurrentFloor;
                var next    = (uint) (current + Step(state));

                return state.With(direction: direction, currentFloor: next);
            }
        }

        internal class Timeout : ElevatorTransition
        {
            public override bool Applicable(Elevator state, ElevatorInput input)
            {
                return input is Internal.Timeout 
                    && (state.Doors == Elevator.Door.Opening
                    || state.Doors == Elevator.Door.Closing 
                    || state.Doors == Elevator.Door.Open);
            }

            public override Elevator Traverse(Elevator state, ElevatorInput input)
            {
                var timeout = (Internal.Timeout) input;

                switch (state.Doors)
                {
                    case Elevator.Door.Opening: return state.With(door: Elevator.Door.Open);
                    case Elevator.Door.Open:    return state.With(door: Elevator.Door.Closing);
                    case Elevator.Door.Closing: return state.With(door: Elevator.Door.Closed);
                }

                return state;
            }
        }
    }
}
