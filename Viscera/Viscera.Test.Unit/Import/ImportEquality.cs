using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viscera.Test.Machinery.Extensions;

namespace Viscera.Test.Unit.Import
{
    [TestClass]
    public class ImportEquality
    {
        [TestMethod]
        public void WeCanTakeOrdersFromRgbOrCmyk()
        {
            using(var rgbStream = "10,255,0,0".AsStream())
            using (var cmykStream = "10,0,100,100,0".AsStream())
            {
                var rgbPaints = UseCases.Import.Rgba(rgbStream);
                var cmykPaints = UseCases.Import.Cmyk(cmykStream);

                Assert.AreEqual(1, rgbPaints.Count());
                Assert.AreEqual(1, cmykPaints.Count());
                //Assert.AreEqual(2, orders.RedCount());
            }
        }

        [TestMethod]
        public void RedInRgbIsTheSameAsRedInCmyk()
        {
            using (var rgbStream = "10,255,0,0".AsStream())
            using (var cmykStream = "10,0,100,100,0".AsStream())
            {
                var rgbPaints = UseCases.Import.Rgba(rgbStream);
                var cmykPaints = UseCases.Import.Cmyk(cmykStream);

                Assert.AreEqual(rgbPaints.First(), cmykPaints.First());
            }
        }

        [TestMethod]
        public void AndSoIsGreenInBoth()
        {
            using (var rgbStream = "10,0,255,0".AsStream())
            using (var cmykStream = "10,100,0,100,0".AsStream())
            {
                var rgbPaints = UseCases.Import.Rgba(rgbStream);
                var cmykPaints = UseCases.Import.Cmyk(cmykStream);

                Assert.AreEqual(rgbPaints.First(), cmykPaints.First());
            }
        }
    }
}
