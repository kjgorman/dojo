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