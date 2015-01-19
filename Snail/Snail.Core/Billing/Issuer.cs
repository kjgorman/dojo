using System;
using Snail.Core.Billing.Documents;
using Snail.Core.Ports;

namespace Snail.Core.Billing
{
    public interface IIssuer
    {
        IDocument IssueDocument(DocumentType type, IDocumentParameters parameters);
    }

    public class Issuer : IIssuer
    {
        private readonly IQuoteProvider _quoteProvider;

        public Issuer(IQuoteProvider quoteProvider)
        {
            _quoteProvider = quoteProvider;
        }

        public IDocument IssueDocument(DocumentType type, IDocumentParameters parameters)
        {
            // common parameters, assume if we don't have these in parameters it will throw
            // exception here
            var id         = parameters.Id();
            var amount     = parameters.Amount();
            var issueDate  = parameters.IssueDate();
            var customerId = parameters.CustomerId();

            if (type == DocumentType.Quote)
            {
                var shipmentDate      = parameters.ShipmentDate();
                var customerAgreement = parameters.CustomerAgreement();

                if (customerAgreement.HasValue && !shipmentDate.HasValue)
                {
                    throw new InvalidOperationException("A quote must have a shipment date before a customer can agree to it!");
                }

                if (shipmentDate.HasValue && shipmentDate.Value < issueDate)
                {
                    throw new InvalidOperationException("You may not issue a quote retrospectively");
                }

                return new Quote
                {
                    Id = id,
                    Amount = amount,
                    IssueDate = issueDate,
                    CustomerAgreement = customerAgreement,
                    ShipmentDate = shipmentDate,
                    CustomerId = customerId
                };
            }

            if (type == DocumentType.BillOfLading)
            {
                var shipmentDate     = parameters.ShipmentDate();
                var parentDocumentId = parameters.ParentDocumentId();

                if (false == shipmentDate.HasValue)
                {
                    throw new InvalidOperationException("You cannot create a bill of lading for something that has not been shipped");
                }

                if (false == parentDocumentId.HasValue)
                {
                    throw new InvalidOperationException("You can only create a bill of lading when it references its parent quote");
                }

                var parentQuote = _quoteProvider.ById(parentDocumentId.Value);

                if (parentQuote == null)
                {
                    throw new InvalidOperationException("A bill of ladings parent document id must already exist before the bill is created");
                }

                if (parentQuote.CustomerId != customerId)
                {
                    throw new InvalidOperationException("The bill of ladings's parent document must refer to the same customer!");
                }

                if (parentQuote.ShipmentDate > issueDate)
                {
                    throw new InvalidOperationException("The bill of lading must be before or on the day of shipment");
                }

                return new BillOfLading
                {
                    Id = id,
                    Amount = amount,
                    IssueDate = issueDate,
                    ShipmentDate = shipmentDate.Value,
                    CustomerId = customerId
                };
            }

            return null;
        }
    }
}
