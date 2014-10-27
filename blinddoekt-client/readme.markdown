### blinddoekt

This is a blind-folded race, where your team consists of a diver and a
navigator. Your navigator is able to query an API to find out your
current location, and the current surrounding map. Your driver is able
to send a movement command to the server.

Between the two roles, you must attempt to traverse from top to bottom
a map that becomes increasingly more densely filled with obstacles.

Here's a diagram:

```
          ---------
         |   API   |<-
          ---------    \
  view   /              \ step
        /                \
  -----v----- <----       \
 | navigator |     \ --------
  ----------- \     | driver |
               ----> --------
            'teamwork'
```

The server has the following endpoints:

  * PUT | register/(name) | registers your team with the server

  * GET | /(hash)/whereami | returns the current team position as a
    javascript object of the form `{ row: <int>, col: <int> }`

  * GET | /(hash)/view | returns the surrounding 10 rows (within the
    grid) in the form of a row major integer encoded list.

  * GET | /whoami/(name) | given a team name, return your role in the
    team, along with some team metadata

  * PUT | /(hash)/step | given a list of positions in the form `{ row:
    <int>, col: <in> }` the server applies at most 20 valid steps,
    before returning the remainder of the given path that either
    exceeded the given length, or was unable to be completed (due to
    e.g. an invalid movement, or the path being blocked).

The team name is a setting you can define in `prod.json` or
`dev.json`. The hash parameter is a unique hash given to the driver
and navigator of each team to determine if the requester is allowed to
perform the given action (i.e. you can only PUT data to the step
endpoint if you have the driver hash). This is mainly handled for you
with the client, but is included for completeness.

In the configuration json files you must also define the api and your
teammates internal IP addresses so you can actually communicate with
them (we'll sort this out on the day probably).

### What to actually do

#### Prerequisites

Everything written here is so far a node app. Feel free to rewrite
everything from scratch in whatever language you want (we're just
sending JSON around so it should be relatively agnostic), but I'd
probably recommend just using this one.

Anyway, I assume you have `node` and `npm` on your path (you should
have this set up already for the fed build process).

The first step is to run `npm install` to download the development
dependencies.

Next, you should be able to run the server with `node index.js
prod.json`.

### Setting up your team

At this point you should be able to hit `http://localhost:3002/ping`
to ensure your teammates server is alive and accessible (or a 'no
response' if they aren't).

Now that you have the server running, you can register your team with
the API, by visiting `http://localhost:3002/register`.

**N.B. the first person in the pair to register is the DRIVER, the
 second is the NAVIGATOR. Make sure you know who wants to do what
 before you register** (if you fuck it up just change your team name
 and re-register in the right order).

Once you've registered you should be able to hit
`http://localhost:3002/whoami` to get some metadata about yourself.

### The rate limit

To kind of overbearingly make a point, the API has a per team rate
limit of 1 minute between requests. This is an attempt to stop brute
forcing, and also to encourage you to write unit tests for your stuff
before trying it out against the "production" API.

### What to actually solve

#### If you're the driver

The major problem you're trying to solve as the driver is to increase
your row counter as quickly as possible (i.e. go from the top to the
bottom of the map).

At the beginning the map is very sparse, so something like just trying
to move down every step would make sense.

As the map becomes denser you will probably need to come up with some
sort of algorithm to find better paths (Djikstra's would be a good
one, but brute force or simpler algorithms like a breadth first search
might be acceptable intermediates).

**N.B. as the driver you can only move in the cardinal directions
  (i.e. up/down/left/right).** This is for simplicities sake.

To actually get started you can use manual input, [in the
routes](https://github.com/kjgorman/dojo/blob/fe075d0fadd382e8f053d168503331ef7da93a28/blinddoekt-client/app/routes.js#L45-L47)
you'll notice visiting `http://localhost:3002/step` will invoke the
application's step command and echo the results.

You'll want to implement your app's [step
method](https://github.com/kjgorman/dojo/blob/fe075d0fadd382e8f053d168503331ef7da93a28/blinddoekt-client/app/app.js#L40-L46)
to produce a path.

#### If you're the navigator

You may have noticed in the API description I said that getting the
surrounding view returns a "row major integer list". This is the bit
where I explain what that actually means.

The map is stored as follows:

```
10000 ...(62 0s)... 00001
10000 ...           00001
1001000010...110111000001
```

That is, as a grid of binary values, where 0 means 'passable' and 1
means 'impassable' (e.g. note that there will always be 'walls' of 1s
along the sides).

The width of the map will be fixed at 64 bits, and communicated to you
in 32 bit integers.

This means that a row like `1...(all 0s)...1` will be encoded as
`[Math.pow(2, 31), 1]` (that is the two 32 bit substrings are each one
integer).

The list will be row major, meaning that the numbers are ordered first
by row, then by column. So a list like `[a, b, c, d]` corresponds to
the grid

```
  a  b

  c  d
```

If that makes sense.

Anyway, your role as the navigator is to decode the API response into
a more readable format, and to build up your view of the map as you
move down the map (remember you can only get the surrounding 10 rows
at a time).

You are then responsible for sending your decoded map to your driver
(or perhaps responding with the map when your driver asks for it).

You are also the only one of the pair who has access to your actual
location, using the `http://localhost:3002/whereami` end-point, which
is obviously crucial!

Anyway, that basically summarises it, as the navigator you store the
map and the position, and pass it on to the driver as is necessary.