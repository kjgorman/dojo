using System.Linq;
using Ascensor.Internal;
using Ascensor.Machinery;
using NUnit.Framework;

namespace Ascensor.Test
{
    public class Requests
    {
        [Test]
        public void ARequestFromTheCurrentFloorWillOpenTheDoorsIfTheyWereClosed()
        {
            var target = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = 0,
                Direction = Elevator.Movement.Idle,
                Doors = Elevator.Door.Opening
            };

            var machine = new Machine<Elevator, ElevatorInput>(target);
            const int pathLimit = 2;

            var path = machine.RunToCompletion(pathLimit);

            // given the input: Request[0]
            // we should trigger a transition from
            // idle/closed/0 -> idle/opening/0/0

            Assert.AreEqual(pathLimit, path.Count);
        }

        [Test]
        public void ARequestFromAFloorAboveWillTriggerAnOpeningClosingSequence()
        {
            var first = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = 1,
                Direction = Elevator.Movement.Idle,
                Doors = Elevator.Door.Opening
            };

            var second = first.With(door: Elevator.Door.Closed);

            var machine = new Machine<Elevator, ElevatorInput>(first, second);
            const int pathLimit = 6;

            var path = machine.RunToCompletion(pathLimit);

            Assert.AreEqual(pathLimit, path.Count);
        }

        [Test]
        public void ARequestFromAFloorAboveTheCurrentOneWillBeginMovingInThatDirectionIfTheDoorsAreClosed()
        {
            var target = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = 1,
                Direction = Elevator.Movement.Up,
                Doors = Elevator.Door.Closed
            };

            var machine = new Machine<Elevator, ElevatorInput>(target);
            const int pathLimit = 6;

            var path = machine.RunToCompletion(pathLimit);

            // given the input: Request[1]
            // we should trigger a transition from
            // idle/closed/0 -> idle/opening/0/1 -> open -> closing -> closed -> up/closed/0/1

            Assert.AreEqual(pathLimit, path.Count);
        }

        [Test]
        public void ThereShouldNotBeAWayForTheDoorsToBeOpenAndNotHaveAnActiveRequest()
        {
            var first = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = null,
                Direction = Elevator.Movement.Idle,
                Doors = Elevator.Door.Open
            };

            var machine = new Machine<Elevator, ElevatorInput>(first);
            const int pathLimit = 10;

            Assert.Throws<Machine.Unsatisfiable>(() => machine.RunToCompletion(pathLimit));
        }

        [Test]
        public void WeShouldBeAbleToGetToAnIdleStateOnTheFirstFloor()
        {
            var firstFloor = new Elevator
            {
                CurrentFloor = 1,
                RequestedFloor = null,
                Direction = Elevator.Movement.Idle,
                Doors = Elevator.Door.Closed
            };

            var machine = new Machine<Elevator, ElevatorInput>(firstFloor);

            const int pathLimit = 15;

            var path = machine.RunToCompletion(pathLimit);

            Assert.AreEqual(pathLimit, path.Count);
        }

        [Test]
        public void WeShouldBeAbleToRoundTripFromTheFirstBackToGround()
        {
            var firstFloor = new Elevator
            {
                CurrentFloor = 2,
                RequestedFloor = null,
                Direction = Elevator.Movement.Idle,
                Doors = Elevator.Door.Closed
            };

            var ground = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = null,
                Direction = Elevator.Movement.Idle,
                Doors = Elevator.Door.Closed
            };

            var machine = new Machine<Elevator, ElevatorInput>(firstFloor, ground);

            const int pathLimit = 20;

            var path = machine.RunToCompletion(pathLimit);

            Assert.AreEqual(ground, path.Last());
            Assert.AreEqual(21, path.Count);
        }

        [Test]
        public void IfWeAskForTheShortestPathWeShouldGetIt()
        {
            var firstFloor = new Elevator
            {
                CurrentFloor = 2,
                RequestedFloor = null,
                Direction = Elevator.Movement.Idle,
                Doors = Elevator.Door.Closed
            };

            var ground = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = null,
                Direction = Elevator.Movement.Idle,
                Doors = Elevator.Door.Closed
            };

            var machine = new Machine<Elevator, ElevatorInput>(firstFloor, ground);

            const int pathLimit = 20;

            var path = machine.RunToCompletion(pathLimit, shortest:true);

            Assert.AreEqual(ground, path.Last());
            Assert.AreEqual(18, path.Count);
        }
    }
}
