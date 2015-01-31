using NUnit.Framework;
using Snail.Core.Shipping;
using Snail.Core.Shipping.Containers;

namespace Snail.Test.Unit.Containers
{
    public class FifoContainerTests
    {
        [Test]
        public void AContainerRequiresANonZeroCapacity()
        {
            new ContainerSpecifications(i => new FifoContainer(i)).AContainerRequiresANonZeroCapacity();
        }

        [Test]
        public void AContainerMayNotHaveMoreElementsThanTheNominatedCapacity()
        {
            new ContainerSpecifications(i => new FifoContainer(i)).AContainerMayNotHaveMoreElementsThanTheNominatedCapacity();
        }

        [Test]
        public void AContainerCanReportItsFillFactorAsARatioOfElementsAddedToCapacity()
        {
            new ContainerSpecifications(i => new FifoContainer(i)).AContainerCanReportItsFillFactorAsARatioOfElementsAddedToCapacity();
        }

        [Test]
        public void CannotCallGetOnAnEmptyContainer()
        {
            new ContainerSpecifications(i => new FifoContainer(i)).CannotCallGetOnAnEmptyContainer();
        }

        [Test]
        public void OnceIPutSomethingInTheContainerICanGetItOutAgain()
        {
            new ContainerSpecifications(i => new FifoContainer(i)).OnceIPutSomethingInTheContainerICanGetItOutAgain();
        }

        [Test]
        public void TakingSomethingOutOfTheContainerWillReduceItsFillFactor()
        {
            new ContainerSpecifications(i => new FifoContainer(i)).TakingSomethingOutOfTheContainerWillReduceItsFillFactor();
        }

        [Test]
        public void AFifoContainerGetsThingsFromTheOtherEndOfTheContainer()
        {
            var container = new FifoContainer(2);
            var first     = new Cargo(1L, 10, HazMatCode.Harmless);
            var second    = new Cargo(2L, 10, HazMatCode.Harmless);

            container.Add(first);
            container.Add(second);

            Assert.That(container.Get(), Is.EqualTo(first));
        }
    }
}
