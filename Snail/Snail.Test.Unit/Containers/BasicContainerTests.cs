using System;
using NUnit.Framework;
using Snail.Core.Shipping;
using Snail.Core.Shipping.Containers;

namespace Snail.Test.Unit.Containers
{
    public class BasicContainerTests
    {
        // I assume there's a better way of doing this, i.e. telling NUnit to run the same
        // test cases with different values for the constructor (in particular I would like
        // to use the [TestCase()] attribute), but nothing wants to play nice with lambda
        // expressions, so for now I'll just do this. If I were using mspec I would be able
        // to use a behaviour, which would have been nice.

        [Test]
        public void AContainerRequiresANonZeroCapacity()
        {
            new ContainerSpecifications(i => new BasicContainer(i)).AContainerRequiresANonZeroCapacity();
        }

        [Test]
        public void AContainerMayNotHaveMoreElementsThanTheNominatedCapacity()
        {
            new ContainerSpecifications(i => new BasicContainer(i)).AContainerMayNotHaveMoreElementsThanTheNominatedCapacity();
        }

        [Test]
        public void AContainerCanReportItsFillFactorAsARatioOfElementsAddedToCapacity()
        {
            new ContainerSpecifications(i => new BasicContainer(i)).AContainerCanReportItsFillFactorAsARatioOfElementsAddedToCapacity();
        }

        [Test]
        public void CannotCallGetOnAnEmptyContainer()
        {
            new ContainerSpecifications(i => new BasicContainer(i)).CannotCallGetOnAnEmptyContainer();
        }

        [Test]
        public void OnceIPutSomethingInTheContainerICanGetItOutAgain()
        {
            new ContainerSpecifications(i => new BasicContainer(i)).OnceIPutSomethingInTheContainerICanGetItOutAgain();
        }

        [Test]
        public void AStandardContainerHasOnlyOneDoorSoTheLastThingIPutInIsTheFirstIGetOut()
        {
            new ContainerSpecifications(i => new BasicContainer(i)).AStandardContainerHasOnlyOneDoorSoTheLastThingIPutInIsTheFirstIGetOut();
        }

        [Test]
        public void TakingSomethingOutOfTheContainerWillReduceItsFillFactor()
        {
            new ContainerSpecifications(i => new BasicContainer(i)).TakingSomethingOutOfTheContainerWillReduceItsFillFactor();
        }

        [Test]
        public void ABasicContainerMustNotHaveTwoHazardousElementsAdjacentToOneAnother()
        {
            var container = new BasicContainer(10);
            // note: I have no idea if zirconium tetrachloride is hazardous or not
            // (in fact I have no idea what it is in general), but just assume that
            // anything with a haz mat code that isn't 'Harmless' is in fact hazardous.
            var hazardous = new Cargo(1L, 10, HazMatCode.ZirconiumTetrachloride);

            container.Add(hazardous);

            // it also doesn't really make sense to say I added something twice, but lets
            // ignore that for now.
            Assert.Throws<InvalidOperationException>(() => container.Add(hazardous));
        }
    }
}
