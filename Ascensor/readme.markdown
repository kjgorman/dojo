### Ascensor

(it means elevator)

For this dojo I thought it would be fun to implement a deterministic
finite state machine to model an elevator moving through a building.

A finite state machine, or finite state automaton (hereafter I'm just
going to write DFA) is a five tuple consisting of:

  * Σ – The input "alphabet"
  * S – The set of states
  * s₀ – An initial state
  * δ – A state transition function, δ: S x Σ -> S (that is, a set of
transitions that for a given state and input, will produce the new
state.)
  * F – A (possibly empty) subset of S that denotes the final states.

You can see my not-quite-mathematical defintion of an abstract DFA in
Ascensor.Machinery.DeterministicFiniteStateMachine.

Anyway, the idea is we are going to try and model a system to map
paths through a building for an elevator, by exploring the state space
of transitions. For example, an elevator like the one defined in
Ascensor.Elevator has four properties: the current floor, the
requested floor (when someone presses a button somewhere), the state
of the doors (open/opening/closing/closed), and the direction of
movement it has (up/down/idle).

(e.g. this image I stole from wikipedia:)

![](http://upload.wikimedia.org/wikipedia/commons/c/cf/Finite_state_machine_example_with_comments.svg)

There are a variety of input that might cause an elevator to
transition between states. The most obvious is a request from someone,
when they press a button on a floor. Some other ones might be timeout
(when the doors have been open for a while, then close), or arriving
at the destination floor.

There are also guards around when an input is applicable to a given
state, for example, an elevator may not be moving while the doors are
open!

The exercises should proceed relatively straightforwardly, I hope.

Basically to start with you have some simple implementation of a
particularly idiotic system of performing essentially a graph search
that satisfies the two tests in InitialStates, and the first test in
Requests.

You're going to have to make the remaining tests in Requests
pass. There's only seven tests there, but implementing them correctly
turns out to be a little tricky.

The idea is that you'll need to first add a timeout event so that you
can implement the sub-DFA the doors model (similar to the diagram I
stole above). Then, once you have the ability to have a request open
and close the doors again you should implement a movement transition
that will allow an elevator with a requested floor to move towards
that floor. Finally, you'll need to add an arrival transition that
says when the elevator arrives at its destination the doors open and
close again.

Then you'll notice that the Machine ctor takes a params array of
targets to iterate through, you should then adjust your traversal
algorithm so you can specify multiple targets sequentially such that
you can request the elevator move to the second floor, then back down
to the ground floor.

Finally, some extra bits that I didn't get around to attempting but
could be fun if you end up breezing through the beginning:

  * memoize path subsequences, so you can store portions of the graph
    in memory rather than walk through them again (the doors sequence
    of opening-open-closing-closed seems like a good candidate)

  * optional breadh and depth first searches – should be easy, just
    swap in a stack or queue for your children iteration in recursion.

  * more clever pruning of the search tree – perhaps assume an
    heuristic that the first time you find a target in a breadth first
    search implies that that path will result in a roughly optimal
    result, and prune all other active traversals in favour of that
    current iteration (i.e. effectively begin again with that path as
    the root of your search).

  * parallelise it I guess... seems like ParallelEnumerable will make
    this real easy.

glhf!

p.s. if things are totally not making any sense and you sort of want
to cheat just a little bit, there's a relatively functional version at
7d4811302ca422459fc15fecb5353da10fb12536. 