using NUnit.Framework;
using Snail.Core.Shipping;
using Snail.Core.Shipping.Containers;

namespace Snail.Test.Unit.Containers
{
    public class ContainerTests
    {
        [Test]
        public void AContainerRequiresANonZeroCapacity()
        {
            new ContainerSpecifications(i => new Container<Cargo>(i)).AContainerRequiresANonZeroCapacity();
        }

        [Test]
        public void AContainerMayNotHaveMoreElementsThanTheNominatedCapacity()
        {
            new ContainerSpecifications(i => new Container<Cargo>(i)).AContainerMayNotHaveMoreElementsThanTheNominatedCapacity();
        }

        [Test]
        public void AContainerCanReportItsFillFactorAsARatioOfElementsAddedToCapacity()
        {
            new ContainerSpecifications(i => new Container<Cargo>(i)).AContainerCanReportItsFillFactorAsARatioOfElementsAddedToCapacity();
        }

        [Test]
        public void CannotCallGetOnAnEmptyContainer()
        {
            new ContainerSpecifications(i => new Container<Cargo>(i)).CannotCallGetOnAnEmptyContainer();
        }

        [Test]
        public void OnceIPutSomethingInTheContainerICanGetItOutAgain()
        {
            new ContainerSpecifications(i => new Container<Cargo>(i)).OnceIPutSomethingInTheContainerICanGetItOutAgain();
        }

        [Test]
        public void AStandardContainerHasOnlyOneDoorSoTheLastThingIPutInIsTheFirstIGetOut()
        {
            new ContainerSpecifications(i => new Container<Cargo>(i)).AStandardContainerHasOnlyOneDoorSoTheLastThingIPutInIsTheFirstIGetOut();
        }

        [Test]
        public void TakingSomethingOutOfTheContainerWillReduceItsFillFactor()
        {
            new ContainerSpecifications(i => new Container<Cargo>(i)).TakingSomethingOutOfTheContainerWillReduceItsFillFactor();
        }
    }
}
