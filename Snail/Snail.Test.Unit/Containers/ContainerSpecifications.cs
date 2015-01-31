using System;
using NUnit.Framework;
using Snail.Core.Shipping;
using Snail.Core.Shipping.Containers;

namespace Snail.Test.Unit.Containers
{
    public class ContainerSpecifications
    {
        private readonly Func<int, Container<Cargo>> _constructor;

        public ContainerSpecifications(Func<int, Container<Cargo>> constructor)
        {
            _constructor = constructor;
        }

        public void AContainerRequiresANonZeroCapacity()
        {
            Assert.Throws<ArgumentException>(() => _constructor(0));
        }

        public void AContainerMayNotHaveMoreElementsThanTheNominatedCapacity()
        {
            var container = _constructor(1);
            container.Add(new Cargo(1L, 10, HazMatCode.Harmless));

            Assert.Throws<InvalidOperationException>(() => container.Add(new Cargo(2L, 10, HazMatCode.Harmless)));
        }

        public void AContainerCanReportItsFillFactorAsARatioOfElementsAddedToCapacity()
        {
            var container = _constructor(10);

            Assert.That(container.FillFactor(), Is.EqualTo(0));

            container.Add(new Cargo(1L, 10, HazMatCode.Harmless));

            Assert.That(container.FillFactor(), Is.EqualTo(0.1m));
        }

        public void CannotCallGetOnAnEmptyContainer()
        {
            var container = _constructor(1);

            Assert.Throws<InvalidOperationException>(() => container.Get());
        }

        public void OnceIPutSomethingInTheContainerICanGetItOutAgain()
        {
            var container = _constructor(1);
            var cargo = new Cargo(1L, 10, HazMatCode.Harmless);

            container.Add(cargo);

            Assert.That(container.Get(), Is.EqualTo(cargo));
        }

        public void AStandardContainerHasOnlyOneDoorSoTheLastThingIPutInIsTheFirstIGetOut()
        {
            var container = _constructor(2);
            var first     = new Cargo(1L, 10, HazMatCode.Harmless);
            var second    = new Cargo(2L, 10, HazMatCode.Harmless);

            container.Add(first);
            container.Add(second);

            Assert.That(container.Get(), Is.EqualTo(second));
        }

        public void TakingSomethingOutOfTheContainerWillReduceItsFillFactor()
        {
            var container = _constructor(10);
            var cargo = new Cargo(1L, 10, HazMatCode.Harmless);

            container.Add(cargo);

            Assert.That(container.FillFactor(), Is.EqualTo(0.1));

            container.Get();

            Assert.That(container.FillFactor(), Is.EqualTo(0));
        }
    }
}
