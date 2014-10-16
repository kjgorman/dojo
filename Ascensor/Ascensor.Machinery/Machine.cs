using System;
using System.Collections.Generic;
using System.Linq;
using Ascensor.Machinery.Lang;

namespace Ascensor.Machinery
{
    public class Machine<TState, TInput>
        where TState : DeterministicFiniteStateMachine<TState, TInput>, new()
    {
        private readonly TState _target;

        public Machine(params TState[] targets)
        {
            if (targets.Length == 0) throw new ArgumentException("requires at most one target");

            // TODO -- hmm... I'm getting params but just ignoring them, for now.
            _target = targets.First();
        }

        private static IEnumerable<TState> Initial { get { return new TState().InitialStates; } }
        private static IEnumerable<TInput> Inputs { get { return new TState().Inputs; } }
        private static IEnumerable<Transition<TState, TInput>> Transitions { get { return new TState().Transitions; } }

        public List<TState> RunToCompletion(int maximumSteps, bool shortest = false)
        {
            // TODO: implement this properly
            // should return the first path to complete for your exploration technique,
            // or throws an unsatisfiable exception while forcing the enumerable.
            var paths = Run(maximumSteps);

            var successful = paths.Where(path => _target.Equals(path.Last())).ToList();

            if (successful.Any())
            {
                return successful.First().ToList();
            }

            throw new Machine.Unsatisfiable();
        }

        // TODO: implement this properly.
        // This should represent all the possible paths we are currently exploring.
        // Once a given path reaches the maximum number of steps, we remove that candidate solution
        // Once we are in a "stuck" state, i.e. there's no longer any path with an applicable
        // transition step, we throw Machine.Unsatisfiable
        public IEnumerable<IEnumerable<TState>> Run(int maximumSteps)
        {
            // [gotcha]: have to use .Equals rather than == as the compiler can't
            // seem to figure out TState is going to be a reference type, despite
            // the constraint on DeterministicFiniteStateMachine

            // TODO -- Wow, this is obviously stupid and will never work for any form of recursion...
            var paths = new List<List<TState>>();

            foreach (var state in Initial)
            {
                if (state.Equals(_target)) return Enumerables.Of(Enumerables.Of(state));

                foreach (var input in Inputs)
                {
                    foreach (var transition in Transitions)
                    {
                        if (transition.Applicable(state, input))
                        {
                            paths.Add(new List<TState> { state, transition.Traverse(state, input) });
                        }
                    }
                }
            }

            return paths;
        }

        // represents the leaf of a stuck path, i.e. we have no where left to go
        // FIXME -- using a null here is shit, but having a concrete stuck type
        // will require a bit of rejigging of the already stupid types so I cbf
        // doing that.
        private static IEnumerable<IEnumerable<TState>> Stuck()
        {
            return Enumerables.Of(Enumerables.Of<TState>(null));
        }
    }

    public static class Machine
    {
        public class Unsatisfiable : Exception
        {
        }
    }
}
