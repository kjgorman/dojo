using System.Collections.Generic;
using System.Linq;
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

        public Quote ById(long quoteId)
        {
            return _query.ByPredicate(model => model.Type == DocumentType.Quote && model.Id == quoteId).FirstOrDefault();
        }
    }
}
