using System.Collections.Generic;
using System.Linq;
using Viscera.Test.Machinery.Extensions;
using Viscera.Test.Machinery.Stubs;

namespace Viscera.Test.Acceptance.Support.Interactors
{
    internal class ImportReceiptingInteractor
    {
        private IEnumerable<Paint.Paint> _state;

        public void Import(string csv)
        {
            _state = UseCases.Import.Rgba(csv.AsStream(), new InMemoryOrderService());
        }

        public string Receipt()
        {
            // this is stupid
            return _state.Aggregate(string.Empty, (receipt, item) => receipt + item.ReceiptLine());
        }
    }
}
