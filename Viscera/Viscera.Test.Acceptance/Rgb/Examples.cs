using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viscera.Test.Acceptance.Support.Interactors;

namespace Viscera.Test.Acceptance.Rgb
{
    [TestClass]
    public class Examples
    {
        private ImportReceiptingInteractor _interactor;

        [TestMethod]
        public void TenLitresOfRedPaint()
        {
            _interactor = new ImportReceiptingInteractor();

            When_I_import_rgb("10,255,0,0");

            Then_I_am_issued_this_receipt("10 litres of red paint");
        }

        private void Then_I_am_issued_this_receipt(string expected)
        {
            Assert.AreEqual(expected, _interactor.Receipt());
        }

        private void When_I_import_rgb(string what)
        {
            _interactor.Import(what);
        }
    }
}
