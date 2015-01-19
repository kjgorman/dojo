using System;

namespace Snail.Core.Billing.Documents
{
    [Serializable]
    public class BillOfLading : IDocument
    {
        public long Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ShipmentDate { get; set; }
        public decimal Amount { get; set; }
        public long CustomerId { get; set; }
    }
}
