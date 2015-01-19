using System;

namespace Snail.Core.Ports
{
    public interface IDocumentParameters
    {
        //required fields
        long Id();
        DateTime IssueDate();
        decimal Amount();
        long CustomerId();
        //optional fields
        DateTime? ShipmentDate();
        DateTime? ReceiptDate();
        bool? CustomerAgreement();
        long? ParentDocumentId();
    }
}
