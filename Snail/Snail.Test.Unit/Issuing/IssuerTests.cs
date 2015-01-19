using System;
using NUnit.Framework;
using Snail.Core.Billing;
using Snail.Core.Billing.Documents;
using Snail.Core.Ports.Exceptions;
using Snail.Test.Machinery.Http;

namespace Snail.Test.Unit.Issuing
{
    public class IssuerTests
    {
        [Test]
        public void CanCreateBasicQuote()
        {
            var issuer = new Issuer();
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow)
                .WithShipmentDate(null)
                .WithCustomerAgreement(null);

            Assert.IsNotNull(issuer.IssueDocument(DocumentType.Quote, parameters));
        }

        [Test]
        public void CreatingAQuoteThatHasntGotARequiredParameterWillThrowTheAppropriateException()
        {
            var issuer = new Issuer();
            var parameters = new FakeRequestParameters();

            Assert.Throws<RequiredParameterNotProvided>(() => issuer.IssueDocument(DocumentType.Quote, parameters));
        }

        [Test]
        public void YouMayNotCreateAQuoteTheCustomerAgreedToIfYouDoNotSpecifyTheShipmentDate()
        {
            var issuer = new Issuer();
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow)
                .WithShipmentDate(null)
                .WithCustomerAgreement(false);

            Assert.Throws<InvalidOperationException>(() => issuer.IssueDocument(DocumentType.Quote, parameters));
        }

        [Test]
        public void YouMayNotCreateAQuoteWhereTheShipmentDateIsBeforeTheIssueDate()
        {
            var issuer = new Issuer();
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow)
                .WithShipmentDate(DateTime.UtcNow.AddDays(-1));

            Assert.Throws<InvalidOperationException>(() => issuer.IssueDocument(DocumentType.Quote, parameters));
        }

        [Test]
        public void CanCreateABasicBillOfLading()
        {
            var issuer = new Issuer();
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithParentDocmentId(3L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow)
                .WithShipmentDate(DateTime.UtcNow);

            Assert.IsNotNull(issuer.IssueDocument(DocumentType.BillOfLading, parameters));
        }

        [Test]
        public void MustProvideAShipmentDateToCreateABillOfLading()
        {
            var issuer = new Issuer();
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow);

            Assert.Throws<InvalidOperationException>(() => issuer.IssueDocument(DocumentType.BillOfLading, parameters));
        }

        [Test]
        public void ABillOfLadingMayOnlyBeCreatedAfterAQuoteHasBeenIssuedToThatCustomer()
        {
            var issuer = new Issuer();
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithParentDocmentId(3L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow)
                .WithShipmentDate(DateTime.UtcNow);

            Assert.Throws<InvalidOperationException>(() => issuer.IssueDocument(DocumentType.BillOfLading, parameters));
        }
    }
}
