using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viscera.Test.Machinery.Assertions;
using Viscera.Test.Machinery.Extensions;
using Viscera.Test.Machinery.Stubs;

namespace Viscera.Test.Unit.Import
{
    [TestClass]
    public class Rgb
    {
        [TestMethod]
        public void ForExampleTenLitresOfRed()
        {
            using (var stream = "10,255,0,0".AsStream())
            {
                var paints = UseCases.Import.Rgba(stream, new InMemoryOrderService());

                Assert.AreEqual(1, paints.Count());
                Assert.AreEqual(10, paints.First().VolumeInLitres);
                Assert.IsTrue(paints.First().IsRed());
            }
        }

        [TestMethod]
        public void ColoursMayNotHaveValueGreaterThan255()
        {
            using (var stream = "10,256,0,0".AsStream())
            {
                var err = Assertions.Throws<Exception>(() => UseCases.Import.Rgba(stream, new InMemoryOrderService()));

                Assert.IsTrue(err.Message.Contains("greater than 255"));
            }
        }

        [TestMethod]
        public void ColoursMayNotHaveValueLessThanZero()
        {
            using (var stream = "10,-1,0,0".AsStream())
            {
                var err = Assertions.Throws<Exception>(() => UseCases.Import.Rgba(stream, new InMemoryOrderService()));

                Assert.IsTrue(err.Message.Contains("less than 0"));
            }
        }

        [TestMethod]
        public void VolumeMayNotBeEqualToZero()
        {
            using (var stream = "0,0,0,0".AsStream())
            {
                var err = Assertions.Throws<Exception>(() => UseCases.Import.Rgba(stream, new InMemoryOrderService()));

                Assert.IsTrue(err.Message.Contains("Volume must be greater than zero"), "Expected the error <{0}> to contain <Volume must be greater than zero>", err.Message);
            }
        }

        [TestMethod]
        public void CanImportTwoColours()
        {
            const string palette = @"
                10,255,0,0
                10,0,255,0
            ";

            using (var stream = palette.AsStream())
            {
                var paints = UseCases.Import.Rgba(stream, new InMemoryOrderService()).ToList();

                Assert.AreEqual(2, paints.Count());
                Assert.IsTrue(paints.First().IsRed());
                Assert.IsTrue(paints[1].IsGreen());
            }
        }

        [TestMethod]
        public void ItRecordsEachOrder()
        {
            using (var stream = "10,255,0,0".AsStream())
            {
                UseCases.Import.Rgba(stream, new InMemoryOrderService());
                /*
                 * for some `orders`...
                Assert.AreEqual(1, orders.RedCount());
                Assert.AreEqual(0, orders.GreenCount());
                Assert.AreEqual(0, orders.BlueCount());
                 */
            }
        }

        [TestMethod]
        public void EachLineMayIncludeWhitespace()
        {
            using (var stream = " 10, 255,  0,   0 ".AsStream())
            {
                try
                {
                    var paints = UseCases.Import.Rgba(stream, new InMemoryOrderService());

                    Assert.IsTrue(paints.First().IsRed());
                }
                catch (Exception e)
                {
                    Assert.Fail("Expected no error, got <{0}>", e.Message);
                }
            }
        }

        // TEST: Alpha is always 100, no matter what you supply
        // TEST: What about duplicates?
        // TEST: What about empty lines?
    }
}
