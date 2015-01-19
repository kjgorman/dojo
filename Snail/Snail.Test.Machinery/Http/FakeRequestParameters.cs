using System;
using Snail.Core.Ports;
using Snail.Core.Ports.Exceptions;

namespace Snail.Test.Machinery.Http
{
    public class FakeRequestParameters : IDocumentParameters
    {
        private long? _id;
        private DateTime? _issueDate;
        private decimal? _amount;
        private long? _customerId;
        private DateTime? _shipmentDate;
        private DateTime? _receiptDate;
        private bool? _customerAgreement;
        private long? _parentDocumentId;

        public FakeRequestParameters WithId(long id)
        {
            _id = id;
            return this;
        }

        public FakeRequestParameters WithIssueDate(DateTime issueDate)
        {
            _issueDate = issueDate;
            return this;
        }

        public FakeRequestParameters WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public FakeRequestParameters WithShipmentDate(DateTime? shipmentDate)
        {
            _shipmentDate = shipmentDate;
            return this;
        }

        public FakeRequestParameters WithCustomerId(long? customerId)
        {
            _customerId = customerId;
            return this;
        }

        public FakeRequestParameters WithParentDocmentId(long? parentDocumentId)
        {
            _parentDocumentId = parentDocumentId;
            return this;
        }

        public FakeRequestParameters WithReceiptDate(DateTime? receiptDate)
        {
            _receiptDate = receiptDate;
            return this;
        }

        public FakeRequestParameters WithCustomerAgreement(bool? customerAgreement)
        {
            _customerAgreement = customerAgreement;
            return this;
        }

        private T Extract<T>(T? property, string propertyName)
            where T : struct
        {
            if (property.HasValue) return property.Value;

            throw new RequiredParameterNotProvided(propertyName);
        }

        public long Id()                 { return Extract(_id, "Id"); }
        public decimal Amount()          { return Extract(_amount, "Amount"); }
        public DateTime IssueDate()      { return Extract(_issueDate, "IssueDate"); }
        public long CustomerId()         { return Extract(_customerId, "CustomerId");}
        public DateTime? ReceiptDate()   { return _receiptDate; }
        public DateTime? ShipmentDate()  { return _shipmentDate; }
        public long? ParentDocumentId()  { return _parentDocumentId; }
        public bool? CustomerAgreement() { return _customerAgreement; }
    }
}
