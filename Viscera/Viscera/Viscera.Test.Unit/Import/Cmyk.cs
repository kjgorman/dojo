using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viscera.Test.Machinery.Assertions;
using Viscera.Test.Machinery.Extensions;

namespace Viscera.Test.Unit.Import
{
    [TestClass]
    public class Cmyk
    {
        [TestMethod]
        public void ForExampleTenLitresOfRed()
        {
            using (var stream = "10,0,100,100,0".AsStream())
            {
                var paints = UseCases.Import.Cmyk(stream);

                Assert.AreEqual(1, paints.Count());
                Assert.AreEqual(10, paints.First().VolumeInLitres);
                Assert.IsTrue(paints.First().IsRed());
            }
        }

        [TestMethod]
        public void ColoursMayNotHaveValueGreaterThan100()
        {
            using (var stream = "10,101,0,0,0".AsStream())
            {
                var err = Assertions.Throws<Exception>(() => UseCases.Import.Cmyk(stream));

                Assert.IsTrue(err.Message.Contains("CMYK values must be <= 100"));
            }
        }

        [TestMethod]
        public void ColoursMayNotHaveValueLessThanZero()
        {
            using (var stream = "10,-1,0,0,0".AsStream())
            {
                var err = Assertions.Throws<Exception>(() => UseCases.Import.Cmyk(stream));

                Assert.IsTrue(err.Message.Contains("CMYK values must be >= 0"));
            }
        }

        [TestMethod]
        public void CmykRequiresFourChannels()
        {
            using (var stream = "10, 0, 0, 0".AsStream())
            {
                Assertions.Throws<Exception>(() => UseCases.Import.Cmyk(stream));
            }
        }

        [TestMethod]
        public void CanImportTwoColours()
        {
            const string palette = @"
                10,0,100,100,0
                10,100,0,100,0
            ";

            using (var stream = palette.AsStream())
            {
                var paints = UseCases.Import.Cmyk(stream).ToList();

                Assert.AreEqual(2, paints.Count());
                Assert.IsTrue(paints.First().IsRed());
                Assert.IsTrue(paints[1].IsGreen());
            }
        }

        [TestMethod]
        public void ItRecordsEachOrder()
        {
            using (var stream = "10,0,100,100,0".AsStream())
            {
                //var orders = 

                UseCases.Import.Cmyk(stream);
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
            using (var stream = " 10, 0,  100,  100,   0 ".AsStream())
            {
                try
                {
                    var paints = UseCases.Import.Cmyk(stream);

                    Assert.IsTrue(paints.First().IsRed());
                }
                catch (Exception e)
                {
                    Assert.Fail("Expected no error, got <{0}>", e.Message);
                }
            }
        }
    }
}
