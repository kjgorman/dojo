### Viscera

Basically the deal is that you're implementing an automatic paint ordering system.

Given a .csv file requesting some paint, whose colour can be specified in either
RGB or CMYK colour schemes, you need to convert the input to the "legacy system"'s
canonical representation. Then, you must return a human readable receipt.

See [the acceptance tests for an example.](https://github.dev.xero.com/Labs/Dojo/blob/master/Viscera/Viscera.Test.Acceptance/Rgb/Examples.cs)

See [the unit tests for conversion specs.](https://github.dev.xero.com/Labs/Dojo/blob/master/Viscera/Viscera.Test.Unit/Import/Cmyk.cs#L15-L23)
