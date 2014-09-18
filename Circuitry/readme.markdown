### circuitry

A circuit diagram is made up of one to many _gates_, connected by _wires_.

Wires carry signals, and gates manipulate signals as they transition through connected wires.

![](http://upload.wikimedia.org/wikipedia/commons/d/d9/Half_Adder.svg)

#### what to do

Unlike the viscera sessions where there was already some implementation and tests,
this is going to be (hopefully) more test driven from the beginning. Instead of filling
out literal tests there's just going to be a list of things to do, in a rough order of
relevance.

- Add an AND gate
- Add a change handler that detects a wire flipping its signal using c#s `event` registration
- Add a probe that records the signal on a wire sequentially
- Add an OR gate
- Add an XOR gate
- Make the gates have a delay in processing time using Task.Delay(), and make operations
  combining them asynchronous using the 4.5 async/await keywords
- Implement a half adder
- Implement a full adder
- Implement an abstraction from in-built shorts to an ordered sequence of wires
- Implement a ripple carry adder that can add shorts
- Implement a small DSL for summation that converts an expression in the form of a string
  e.g. "2 + 6 + 8" to postfix through a stack, and evaluates the expression with the ripple
  carry adder.
