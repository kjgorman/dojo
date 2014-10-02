### aquifer

This example is more or less stolen from Carlo Pescio's blog post
["Life without controllers (part
1)"](http://www.carlopescio.com/2012/03/life-without-controller-case-1.html).

![aquifer](http://2.bp.blogspot.com/-P5Gd0gjuyj0/T14sJEhB0cI/AAAAAAAAAGQ/5AWVW_uGecE/s400/mine.png)

The idea is basically to model a pump controller in a mine sump. You
have a pump, some quantity of water at the bottom of your sump, and
some sensors for overflow, underflow, methane content, monoxide
content, and airflow.

If the water level falls below the underflow sensor then the pump
releases some volume back into the sump.

If the water level rises above the overflow sensor then the pump
extracts water from the sump to... somewhere else I guess.

If there's "critical" levels of methane, monoxide, or (a critical lack
of) air flow, then an alarm should be sounded.

Additionally, one cannot operate the pump if there's a critical amount
of monoxide, as we would risk causing the gas to combust.

##### what we're actually doing

I was thinking we haven't done anything sufficiently free-form to
allow people to deviate from one another in a way that would be
interesting to talk about, so this one is deliberately left vague so
you can restructure as you want.

Hopefully you've read Yegge's and Pescio's rants on naming things, and
you can think about the conceptual framework you can use to model the
system.

I've deliberately left how exactly we measure the values from any of
the sensors vague -- you can assume it's an unmanageable IO action
though. Additionally it's not clear how the action of the pump should
influence the sensors, but it's obvious that it should, somehow. It's
also not clear exactly for whom the alarm tolls (except Hemingway,
maybe), but it's obvious that it should notify _something_.

I think building a conceptual model of communication is also
interesting here, because it will probably arise as a result of
emergent design of the
nouns/verbs/objects/domains/behaviours/etc. that we actually end up
modelling with.

So you should feel free to remove all the classes that are present for
example purposes and to re-write as you feel. You'll probably also
need to take some artistic license with how "ticks" of a control
routine should work (i.e. at the moment it's polling, but you might
need some kind of abstract scheduler or event system to actually model
the passage of time). Feel free to make that as elaborate or as simple
as you want, as long as it's not so obviously stupid it detracts from
some of the essential complexity of the rest of the problem... but
obviously that is a subject measure so you'll probably need to play it
by ear.

That sounds pretty vague now writing it out, hopefully this isn't
shit... :pray:

I guess the idea is more or less, take a second to think conceptually
about the domain and problems you're attempting to model, but then use
TDD to grow the program, and see what happens.