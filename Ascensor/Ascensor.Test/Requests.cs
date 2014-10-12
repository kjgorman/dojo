using Ascensor.Inputs;
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
            const int pathLimit = 2;

            var path = machine.RunToCompletion(pathLimit);

            // given the input: Request[1]
            // we should trigger a transition from
            // idle/closed/0 -> up/closed/0/1

            Assert.AreEqual(pathLimit, path.Count);
        }

        [Test]
        public void ButIfThereIsARequestAndWeHaveDoorsThatArentClosedWeNeedToTriggerASubsequenceToCloseThem()
        {
            var first = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = null,
                Direction = Elevator.Movement.Idle,
                Doors = Elevator.Door.Open
            };

            var second = new Elevator
            {
                CurrentFloor = 0,
                RequestedFloor = 1,
                Direction = Elevator.Movement.Up,
                Doors = Elevator.Door.Closed
            };

            var machine = new Machine<Elevator, ElevatorInput>(first, second);
            const int pathLimit = 8;

            var path = machine.RunToCompletion(pathLimit);

            // closed -> request[0] -> opening -> open (request satisfied) -> closing -> closed -> request[1] -> moving up

            Assert.AreEqual(pathLimit, path.Count);
        }
    }
}
