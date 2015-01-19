using System.Collections.Generic;
using Snail.Adapters.DataAccess;
using Snail.Adapters.Model;
using Snail.Core.Billing.Documents;
using Snail.Core.Ports;

namespace Snail.Adapters.Providers
{
    public class QuoteProvider : IQuoteProvider
    {
        private readonly IBoxingQuery<DocumentModel, Quote> _query;

        public QuoteProvider(IBoxingQuery<DocumentModel, Quote> query)
        {
            _query = query;
        }

        public IEnumerable<Quote> All()
        {
            return _query.ByPredicate(model => model.Type == DocumentType.Quote);
        }
    }
}
