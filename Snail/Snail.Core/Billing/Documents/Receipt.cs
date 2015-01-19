using System;

namespace Snail.Core.Billing.Documents
{
    [Serializable]
    public class Receipt : IDocument
    {
        public long Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal Amount { get; set; }
        public long CustomerId { get; set; }
    }
}
