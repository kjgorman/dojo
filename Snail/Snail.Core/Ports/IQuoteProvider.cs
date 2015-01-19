using System.Collections.Generic;
using Snail.Core.Billing.Documents;

namespace Snail.Core.Ports
{
    public interface IQuoteProvider
    {
        IEnumerable<Quote> All();
    }
}
