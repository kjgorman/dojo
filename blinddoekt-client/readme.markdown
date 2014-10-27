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

#### Both of you

So obviously the point of this being a blind-folded race is that you
only have partial knowledge at a time and need to share that between
one another.

This means you need to decide on a format for the map the navigator
passes to the driver (e.g. the navigator could decode the integers
back into a 2d array of 0s and 1s, and send you a position coordinate,
or they could perhaps actually encode a graph structure with an
adjacency matrix or something with relative movement encoded in the
edges).

You also need to figure out how to tell one another when an action has
happened (i.e. the driver sends a PUT, and tells the navigator to
update their position and map). This could just be manual at the
beginning, i.e. you actually turn around and ask them to update in
real life, but it would be cool to get a feedback loop running between
the servers as well.

### I have no idea what's going on

The actual server is also in the dojo folder, and can be run on local
if you follow the same `npm install && node index.js dev.json`
instructions.

It also has some examples of how to write simple unit tests in node.js
(I would recommend also installing mocha as a test runner using `npm
install -g mocha`).

It also has the implementation of the map encoding, and a simple
breadth first search through the map (to ensure an actual path
exists!) which you can probably steal/reverse engineer into something
that would work to traverse it.

### Probably a not unreasonable approach to this

Outside of actually installing node and registering your team,
probably the first step to getting through this is getting a handle on
the map data you have, and establishing and end-point your driver can
hit to retrieve information from.

The request API we're using uses Promises to give a relatively
compositional approach to the asynchronous actions. If you don't know
anything about Promises, the main thing you'll need to know is the
`then` function to chain together promises. For example, the navigator
will hit the /view endpoint on their local client, and it will make an
HTTP request for the encoded state and return a promise that will
resolve to the response [on this
line](https://github.com/kjgorman/dojo/blob/fe075d0fadd382e8f053d168503331ef7da93a28/blinddoekt-client/app/app.js#L35).

If you change it to look like this:

```javascript
if (client.view) {
   return client.view().then(function (data) {
       data = JSON.parse(data)
       //data is now an object that looks like:
       // {
       //    location : { row: <int>, col: <int> },
       //    lower : int,
       //    upper : int,
       //    encoding: [ 2147483648, 1... ]
       // }

       // so in here we can decode the input, update
       // the current location, the current map etc.

       // the data we return will just be echoed to the
       // page, so you could e.g. format it to be human
       // readable or something.
       return data
   })
}
```

The `then` effectively says "when you get resolved to a value, invoke
this function and return that result as a promise. If the value you
return is _another_ promise it "flattens" them both to one promise. In
this case we're just returning some data so it's a moot point, but the
idea is that if you did e.g. another http request to your team member
within the promise you wouldn't have the nesting problem that
callbacks end up with.

Anyway, at this point the navigator can do a request, get some data,
perhaps save it locally then write some unit tests or something to
figure out the decoding process.

The next step is probably to make the current location and known map
available to the driver.

The navigator can add to their routes something like:

```javascript
server.get('/map', function (req, res) {
    res.status(200).send(application.getUpdatedMap())
})
```

then in their app.js:

```javascript
App.prototype.currentStatus = function currentStatus () {
    // I assume in here that you set these scope variables
    // to the `this` object in your `view` function, outlined
    // above.

    // you can decide here maybe what format and information
    // you want to transfer to your team mate
    return {
        location: this.currentLocation,
        map: this.currentMap
    }
}
```

The driver then has to update their application to query your new
endpoint, starting from their routes.js (this isn't strictly
necessary, but follows the pattern of allowing you to hit a url to
perform actions, it could of course be driven by the server)

```javascript
server.get('/map', function (req, res) {
    echo(res, application.getUpdatedMap())
})
```

in their app.js:

```javascript
App.prototype.getUpdatedMap = function getUpdatedMap () {
    var _this = this
    // 'buddy' here is significant, it's a cached 'team' channel...
    return this.client(this.teamMate, 'buddy').getUpdatedMap()
               .then(function (data) {
                   data = JSON.parse(data)
                   // now you have the data on the driver side, e.g.
                   // data will have `location` and `map`, if that's what
                   // was sent, as it is in the above example

                   _this.lastKnownLocation = data.location
                   return data
               }
}
```

Finally in the client.js add an actual request to the client

```javascript
Client.prototype.getUpdatedMap = function getUpdatedMap () {
    return rp({
        uri: this.apiBase + '/map',
        method: 'GET'
    })
}
```

So now the driver should be able to visit `http://localhost:3002/map`
and it should call through to the navigator and return the actual map.

With that, you should now be able to follow those patterns to add any
arbitrary endpoints to communicate between the two of you, as well as
have just enough understanding of promises to use them properly. Which
should be enough to complete at least some of this exercise.

Good luck!