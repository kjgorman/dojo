using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Http;
using Snail.Adapters.Http;
using Snail.Core.Billing;
using Snail.Core.Billing.Documents;
using Snail.Core.Ports;

namespace Snail.Controllers
{
    public class QuotesController : ApiController
    {
        private readonly IQuoteProvider _provider;
        private readonly IIssuer _issuer;

        public QuotesController(IQuoteProvider provider, IIssuer issuer)
        {
            _provider = provider;
            _issuer = issuer;
        }

        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return _provider.All();
        }

        [HttpPut]
        public Quote Create(NameValueCollection form)
        {
            return
                _issuer.IssueDocument(DocumentType.Quote, new DocumentParameters(form)) as Quote;
        }
    }
}
