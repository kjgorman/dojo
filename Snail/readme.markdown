### Snail

Snail is a French cargo shipping company (get it?)

I stole this example of a domain model from page 181 of the domain
driven design "[blue
book](http://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215)"
.

![model](http://i.imgur.com/GRhaX1x.jpg)

The model intends to describe a domain where the company ships cargo
from place to place, for customers who we then invoice.

#### Setting up Snail

To set up snail, first get the latest version of the Dojo repo from
upstream (obviously), then follow the same setup steps as we would for
a standard website in the Xero ecosystem.

That is, first build the solution and make sure I haven't forgotten
some dependencies or didn't include a file in the csproj or something
dumb like that.

Then you can deploy SnailDb locally using the local.publish.xml
file. This will create the database, users, and fill in some dummy
reference data to get started.

Next, add a website to your IIS called `snail.web`, pointing to
`C:\path\to\your\dojo\repo\Snail\Snail`.

Then, you should be able to navigate to
`http://snail.web/api/locations` and get a simple JSON list of the
reference locations that were setup in the database for you.


#### Issue #1: Single Responsibility Pattern

> "Each responsibility should be a separate class, because each
  responsibility is an axis of change."

The first part is divided broadly into two parts. Firstly, there is
some combinatoric explosion in search parameters, and secondly is
measuring the cost of shipping.

To begin with, take a look at the `Get` verb on the
`LocationsController`:

```csharp
        [HttpGet]
        public IEnumerable<Location> Get(string countryName = null, string portName = null)
        {
            if (countryName != null && portName != null)
            {
                return _provider.ByExactLocation(countryName, portName);
            }

            if (countryName != null)
            {
                return _provider.ByCountry(countryName);
            }

            if (portName != null)
            {
                return _provider.ByPort(portName);
            }

            return _provider.All();
        }
```

We can see that it has the ability to search by zero to many
properties of locations, in this those properties are the names of a
location resident country and port.

The idea behind this one is that we want to preserve the url structure
in the method signature, that is we might query
`snail.web/api/locations?countryName=japan&portName=Tokyo` and we
would like those parameters to be converted to individual named
arguments in the method.

The idea is the we now want to add a new parameter,
`locationId`. First, try and copy the existing way of combining
parameters by adding new if statements and query methods to the
repository and service layers.

Hopefully you can see where this would end up: so the next idea is to
think about how we might be able to add each parameter independently
of one another. To do this, remove all of the existing search code and
start again working to the original amount of functionality above
(that is, search by country and port names).

Now try and structure your code so that you can add the location id
without needing to change any code relating to the other
parameters. How easy is it to now remove one of the parameters?

Can you generalise the solution to provide a search capability to
the legs controller/provider as well?

The second part, which I feel we might not get to, is extending the
routing system to have a concept of "cost", such that a customer
agreement might be declined due to their inability to afford any
available route. This one is a more free-form problem, and is defined
only in terms of an unimplemented acceptance test (`Snail.Test.Acceptance.Routes.CanDenyFreeLoaders`).

#### Issue #2 Open/Closed Principle

> Software entities (classes, modules, functions, etc.) should be open for extension, but closed for modification

The 'O' in SOLID stands for the open/closed principle, in my opinion
one of the more ambitious principles in the set. It claims that design
should be optimised such that changing behaviour can be done without
needing to modify existing code. This in my opinion ties closely to the
"ideal" of object oriented programming, that we may be able to construct
systems at the level of abstraction of the object, and not the procedure,
and thus be able to compose and mutate programs solely through addition
and deletion of objects, not through the manipulation of their internals.

However, the precise definition of 'extension' in the principle is not
particularly black and white, and indeed one needs only to look at 
attempts to address the [expression problem](http://c2.com/cgi/wiki?ExpressionProblem)
to see how it might be the case that extension without modification is
in fact not possible with our current tools. Some solutions have been
presented, notably Odersky's [Independently Extensible Solutions to
the Expression Problem](http://scala-lang.org/docu/files/IC_TECH_REPORT_200433.pdf) 
and Oliveira & Cook's [Extensibility for the Masses](https://www.cs.utexas.edu/~wcook/Drafts/2012/ecoop2012.pdf)
(both of which I'd recommend reading, although they're not strictly 
necessary for this problem).

In object-oriented systems polymorphism typically grants us one
dimension of extensibility rather straight-forwardly. To explore
that today our problem will be around the creation of documents that
share an underlying 'single table inheritance' model, but different
invariants about which properties are required.

We will be constructing three different kinds of documents that one
might expect to encounter in our shipping domain: quotes, bills of
lading, and receipts. A quote is a tabled valuation of a service,
which upon customer agreement implies delivery of some product or
service. A bill of lading in shipping is a document that a carrier
issues a customer as a legal guarantee of the shipping of their
cargo. A receipt is issued to a customer upon payment for a service
rendered or product delivered.

In our system we say a relation with a customer proceeds as follows:

* A quote is issued to a customer suggesting an amount and shipment
  date
* After a quote is accepted a bill of lading is issued for the 
  cargo to be shipped
* Once cargo has been delivered, the bill of lading is exchanged
  along with payment for a receipt

To create these documents we use a factory object, called an
[Issuer](https://github.dev.xero.com/Labs/Dojo/blob/master/Snail/Snail.Core/Billing/Issuer.cs)
(in the sense that it issues documents). Given a type of document
required, and some parameters corresponding to the underlying
model, it will attempt to create a document of specified type
while enforcing some simple invariants.

The current design demonstrates some obvious problems, creating
a bill of lading requires knowing about it's previous quote, so
introduces a dependency on `IQuoteProvider`. However, creating
a quote does not depend on this, so we have an unnecessary dependency
for some operations on the object, suggesting it is not very cohesive.

Additionally, and most pertinent for the open/closed principle,
adding a new type of document to be created requires editing the
`IssueDocument` method, rather than simply plugging in a new object.

So, similar to issue #1, the idea will be to first add some functionality
in the same style as the existing code and observe why it is
particularly painful to proceed in this fashion.

The first task is therefore to add creation logic for Receipts to
`IssueDocument` in much the same manner as the prior two documents.
The invariants between a receipt and a bill of lading is similar
to those between a bill of lading and a quote. That is, they must
associate the same customer, the receipt must refer to the bill of
lading through its parent document pointer, the issue date of the
receipt needs to occur at a sensible point of time (presumably
after the issue and shipment dates of the bill of lading). The
receipt must have a `ReceiptDate` and an `IssueDate`, where the
`ReceiptDate` comes after the `IssueDate`. While doing this you
should add test cases to [the issuer tests](https://github.dev.xero.com/Labs/Dojo/blob/master/Snail/Snail.Test.Unit/Issuing/IssuerTests.cs)
and will need to create some kind of `BillOfLadingProvider`.

Once you've got the hang of what's going on and have some tests
that ensure all the reasonable invariants let's start refactoring.
The idea is to redesign the way the factory works such that all
invariants hold for the first two document types. Then, you should
be able to add the third document type _without modifying any
existing code_ (i.e. we should follow the open/closed principle).
The way in which you refactor is more or less up to you. It would
be nice if it was possible to retain the `IssueDocument` signature
and only refactor internals, but perhaps you think that signature
is sufficiently rubbish that you must also change that in order to
get something sensible running, it's up to you.