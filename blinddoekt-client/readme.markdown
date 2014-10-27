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
    grid) in the form of a rank major integer encoded list.

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

At this point you should be able to hit `http://localhost:3002/ping`
to ensure your teammates server is alive and accessible (or a 'no
response' if they aren't).

Now that you have the server running, you can register your team with
the API, by visiting `http://localhost:3002/register`.

*N.B. the first person in the pair to register is the DRIVER, the
 second is the NAVIGATOR. Make sure you know who wants to do what
 before you register * (if you fuck it up just change your team name
 and re-register in the right order).

Once you've registered you should be able to hit
`http://localhost:3002/whoami` to get some metadata about yourself.