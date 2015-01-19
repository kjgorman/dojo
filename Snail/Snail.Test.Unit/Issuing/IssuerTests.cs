using System;
using Moq;
using NUnit.Framework;
using Snail.Core.Billing;
using Snail.Core.Billing.Documents;
using Snail.Core.Ports;
using Snail.Core.Ports.Exceptions;
using Snail.Test.Machinery.Http;

namespace Snail.Test.Unit.Issuing
{
    public class IssuerTests
    {
        [Test]
        public void CanCreateBasicQuote()
        {
            // [!] this seems like a code smell, we can get away with passing a null
            //     into this ctor
            var issuer = new Issuer(null);
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
            var issuer = new Issuer(null);
            var parameters = new FakeRequestParameters();

            Assert.Throws<RequiredParameterNotProvided>(() => issuer.IssueDocument(DocumentType.Quote, parameters));
        }

        [Test]
        public void YouMayNotCreateAQuoteTheCustomerAgreedToIfYouDoNotSpecifyTheShipmentDate()
        {
            var issuer = new Issuer(null);
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
            var issuer = new Issuer(null);
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
            const long parentDocumentId = 3L;
            const long customerId = 2L;
            
            var mockQuoteProvider = new Mock<IQuoteProvider>();
            mockQuoteProvider.Setup(q => q.ById(parentDocumentId)).Returns(new Quote { CustomerId = customerId });

            var issuer = new Issuer(mockQuoteProvider.Object);

            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(customerId)
                .WithParentDocmentId(parentDocumentId)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow)
                .WithShipmentDate(DateTime.UtcNow);

            Assert.IsNotNull(issuer.IssueDocument(DocumentType.BillOfLading, parameters));
        }

        [Test]
        public void MustProvideAShipmentDateToCreateABillOfLading()
        {
            var issuer = new Issuer(null);
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow);

            Assert.Throws<InvalidOperationException>(() => issuer.IssueDocument(DocumentType.BillOfLading, parameters));
        }

        [Test]
        public void MustProvideAParentDocumentIdToCreateABillOfLading()
        {
            var issuer = new Issuer(null);
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithAmount(10)
                .WithShipmentDate(DateTime.UtcNow)
                .WithIssueDate(DateTime.UtcNow);

            Assert.Throws<InvalidOperationException>(() => issuer.IssueDocument(DocumentType.BillOfLading, parameters));
        }

        [Test]
        public void ABillOfLadingMayOnlyBeCreatedAfterAQuoteHasBeenIssuedToThatCustomer()
        {
            var issuer = new Issuer(new Mock<IQuoteProvider>().Object);
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithParentDocmentId(3L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow)
                .WithShipmentDate(DateTime.UtcNow);

            Assert.Throws<InvalidOperationException>(() => issuer.IssueDocument(DocumentType.BillOfLading, parameters));
        }

        [Test]
        public void ABillOfLadingsParentQuoteMustReferToTheSameCustomerAsTheBillItself()
        {
            var quoteProvider = new Mock<IQuoteProvider>();
            quoteProvider.Setup(q => q.ById(It.IsAny<long>())).Returns(new Quote { CustomerId = 999L });

            var issuer = new Issuer(quoteProvider.Object);
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(2L)
                .WithParentDocmentId(3L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow)
                .WithShipmentDate(DateTime.UtcNow);

            Assert.Throws<InvalidOperationException>(() => issuer.IssueDocument(DocumentType.BillOfLading, parameters));
        }

        [Test]
        public void ABillOfLadingMustBeIssuedBeforeOrOnTheDayOfShipmentSpecifiedInItsQuotation()
        {
            const long customerId = 3L;
            var quoteProvider = new Mock<IQuoteProvider>();
            
            quoteProvider
                .Setup(q => q.ById(It.IsAny<long>()))
                .Returns(new Quote {CustomerId = customerId, ShipmentDate = DateTime.UtcNow});

            var issuer = new Issuer(quoteProvider.Object);
            var parameters = new FakeRequestParameters()
                .WithId(1L)
                .WithCustomerId(customerId)
                .WithParentDocmentId(2L)
                .WithAmount(10)
                .WithIssueDate(DateTime.UtcNow.AddDays(-1))
                .WithShipmentDate(DateTime.UtcNow);

            Assert.Throws<InvalidOperationException>(() => issuer.IssueDocument(DocumentType.BillOfLading, parameters));
        }
    }
}
