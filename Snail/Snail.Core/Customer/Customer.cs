using System.Collections.Generic;
using Snail.Core.Lang.Validation;

namespace Snail.Core.Customer
{
    public class Customer
    {
        public readonly IEnumerable<Contact> Details;

        public Customer(IEnumerable<Contact> details)
        {
            Details = details.ValidateNonEmpty();
        }
    }
}
