using System;
using Snail.Core.Billing.Documents;

namespace Snail.Adapters.Model
{
    public class DocumentModel
    {
        public virtual long         Id                { get; set; }
        public virtual DocumentType Type              { get; set; }
        public virtual decimal      Amount            { get; set; }
        public virtual DateTime     IssueDate         { get; set; }
        public virtual long         CustomerId        { get; set; }
        public virtual DateTime?    ReceiptDate       { get; set; }
        public virtual DateTime?    ShipmentDate      { get; set; }
        public virtual long?        ParentDocumentId  { get; set; }
        public virtual bool?        CustomerAgreement { get; set; }
    }
}
