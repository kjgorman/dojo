using System;
using System.Collections.Specialized;
using Snail.Core.Ports;
using Snail.Core.Ports.Exceptions;

namespace Snail.Adapters.Http
{
    public class DocumentParameters : IDocumentParameters
    {
        private readonly NameValueCollection _form;

        public DocumentParameters(NameValueCollection form)
        {
            _form = form;
        }

        public long Id()
        {
            try
            {
                return long.Parse(_form["Id"]);
            }
            catch (ArgumentNullException)
            {
                throw new RequiredParameterNotProvided("Id");
            }
        }

        public DateTime IssueDate()
        {
            try
            {
                return DateTime.Parse(_form["IssueDate"]);
            }
            catch (ArgumentNullException)
            {
                throw new RequiredParameterNotProvided("IssueDate");
            }
        }

        public decimal Amount()
        {
            try
            {
                return Decimal.Parse(_form["Amount"]);
            }
            catch (ArgumentNullException)
            {
                throw new RequiredParameterNotProvided("Amount");
            }
        }

        public long CustomerId()
        {
            try
            {
                return long.Parse(_form["CustomerId"]);
            }
            catch (ArgumentNullException)
            {
                throw new RequiredParameterNotProvided("CustomerId");
            }
        }

        public DateTime? ShipmentDate()
        {
            var date = _form["ShipmentDate"];

            if (date == null) return null;

            return DateTime.Parse(date);
        }

        public DateTime? ReceiptDate()
        {
            var date = _form["ReceiptDate"];

            if (date == null) return null;

            return DateTime.Parse(date);
        }

        public bool? CustomerAgreement()
        {
            var agreement = _form["CustomerAgreement"];

            if (agreement == null) return null;

            return bool.Parse(agreement);
        }

        public long? ParentDocumentId()
        {
            var parentId = _form["ParentDocumentId"];

            if (parentId == null) return null;

            return long.Parse(parentId);
        }
    }
}
