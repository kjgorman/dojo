using System;

namespace Snail.Core.Billing.Documents
{
    public interface IDocument
    {
        long Id { get; }
        DateTime IssueDate { get; }
        decimal Amount { get; }
        long CustomerId { get; }
    }
}
