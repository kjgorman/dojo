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

