using System.Linq;
using Ascensor.Internal;
using Ascensor.Machinery;
using NUnit.Framework;

namespace Ascensor.Test
{
    public class InitialStates
    {
        [Test]
        public void TheTargetStateBeingAValidInitialStateWillReturnImmediatelyWithPathLengthOne()
        {
            var idleAtGround = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = null,
                Doors = Elevator.Door.Closed,
                Direction = Elevator.Movement.Idle
            };
            var m = new Machine<Elevator, ElevatorInput>(idleAtGround);

            const int pathLimit = 1;
            var path = m.RunToCompletion(pathLimit);

            Assert.AreEqual(pathLimit, path.Count());
            Assert.AreEqual(idleAtGround, path.First());
        }

        [Test]
        public void TheTargetStateBeingSomethingThatIsntAValidStateWhenConstrainedToOnePathLengthIsUnsatisfiable()
        {
            var movingUp = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = 0,
                Doors = Elevator.Door.Closed,
                Direction = Elevator.Movement.Up
            };

            var machine = new Machine<Elevator, ElevatorInput>(movingUp);
            const int pathLimit = 1;

            Assert.Throws<Machine.Unsatisfiable>(() => machine.RunToCompletion(pathLimit));
        }
    }
}
