### viscera, _the reissue_

Hopefully you all remember our first exercise, the worlds greatest and most
fully featured paint ordering system, [viscera](https://github.com/kjgorman/dojo/tree/master/Viscera/Viscera).

Well, our clients decided they actually thought that name was terrible, and
fired our .NET developers, to replace them with ruby devs, who decided to rewrite
it, and rename it, to latent.

They started trying to figure out what they were doing:

![](http://i.imgur.com/D37s5AI.png)

You can create importers that take a certain colour encoding (like RGB), and
use them to read paint orders:

![](http://i.imgur.com/IxkSLlJ.png)

Then, our clients and overlords decided they needed some kind of audit log
to record the orders that have been made.

![](http://i.imgur.com/5mHO0EC.png)

However, our shabby ruby devs didn't write it test first, and just whacked the
recorder straight into the importer. Considering we only wanted to expose the
importer factory to our clients to allow us to flexibly change our importer
implementations without the client needing to know more than `RGB` or `CMYK`,
the dependency is now well and truly embedded into the system.

We realised we needed to write some tests to describe some of the business
rules for the recorder, but we can't control our dependency on it:

![](http://i.imgur.com/MJDH3Is.png)

In viscera, we were able to use resharper and our compiler to refactor the
dependency out of the core logic. In Ruby, we don't have the support of a
compiler to let us know that thing like constructors are being invoked with
the correct number of arguments, it will just fail at run time.

So, in this exercise we will:

  * Complete the tests marked TODO be refactoring the importer system to
    allow us to provide an `InMemoryRecorder` (or similar).

  * Extend the recorder to have feature parity to the old c# version, by
    creating some receipting functionality that gives us easy to read 
    descriptions of our orders.

  * Complete the test suite to include some entirely abstract acceptance
    tests, similar to:

![](http://i.imgur.com/qMazz14.png)

Hopefully that makes sense!



