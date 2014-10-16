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
                    new Request(), new Timeout(), new Movement()
                });
            }
        }

        internal class Request : ElevatorTransition
        {
            public override bool Applicable(Elevator state, ElevatorInput input)
            {
                return true;
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

        internal class Movement : ElevatorTransition
        {
            public override bool Applicable(Elevator state, ElevatorInput input)
            {
                return false;
            }

            public override Elevator Traverse(Elevator state, ElevatorInput input)
            {
                throw new NotImplementedException();
            }
        }

        internal class Timeout : ElevatorTransition
        {
            public override bool Applicable(Elevator state, ElevatorInput input)
            {
                return false;
            }

            public override Elevator Traverse(Elevator state, ElevatorInput input)
            {
                throw new NotImplementedException();
            }
        }
    }
}
