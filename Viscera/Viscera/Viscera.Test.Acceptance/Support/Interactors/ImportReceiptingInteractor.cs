using System.Collections.Generic;
using System.Linq;
using Viscera.Test.Machinery.Extensions;

namespace Viscera.Test.Acceptance.Support.Interactors
{
    internal class ImportReceiptingInteractor
    {
        private IEnumerable<Paint.Paint> _state;

        public void Import(string csv)
        {
            _state = UseCases.Import.Rgba(csv.AsStream());
        }

        public string Receipt()
        {
            // this is stupid
            return _state.Aggregate(string.Empty, (receipt, item) => receipt + item.ReceiptLine());
        }
    }
}
