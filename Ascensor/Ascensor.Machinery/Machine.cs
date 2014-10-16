using System;
using System.Collections.Generic;
using System.Linq;
using Ascensor.Machinery.Lang;

namespace Ascensor.Machinery
{
    public class Machine<TState, TInput>
        where TState : DeterministicFiniteStateMachine<TState, TInput>, new()
    {
        private readonly Queue<TState> _targets;
        private TState _target;

        public Machine(params TState[] targets)
        {
            if (targets.Length == 0) throw new ArgumentException("requires at most one target");

            _targets = new Queue<TState>(targets);
            _target = _targets.Dequeue();
        }

        private static IEnumerable<TState> Initial { get { return new TState().InitialStates; } }
        private static IEnumerable<Transition<TState, TInput>> Transitions { get { return new TState().Transitions; } }
        private static IEnumerable<TInput> Inputs { get { return new TState().Inputs; } } 

        public List<TState> RunToCompletion(int maximumSteps)
        {
            // TODO: implement this properly
            // should return the first path to complete for your exploration technique,
            // or throws an unsatisfiable exception while forcing the enumerable.
            return Run(maximumSteps).First().ToList();
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
            // the constraint on NonDeterministicFiniteStateMachine

            var allPaths = Initial.SelectMany(state => RunFrom(state, maximumSteps));

            var successful = allPaths.Where(path => path.Last().Equals(_target)).ToList();

            if (false == successful.Any()) throw new Machine.Unsatisfiable();

            return successful;
        }

        private IEnumerable<IEnumerable<TState>> RunFrom(TState state, int maximumSteps)
        {
            if (maximumSteps == 0) return Enumerables.Of(Enumerable.Empty<TState>());

            var current = Enumerables.Of(state);
            if (state.Equals(_target))
            {
                if (_targets.Count == 0) return Enumerables.Of(current);

                _target = _targets.Dequeue();
                return RunFrom(state, maximumSteps).Select(current.Concat);
            }

            var states = Inputs.SelectMany(input => Transitions
                                            .Where(t => t.Applicable(state, input))
                                            .Select(t => t.Traverse(state, input)));

            return states.SelectMany(next => RunFrom(next, maximumSteps - 1)).Select(current.Concat);
        }
    }

    public static class Machine
    {
        public class Unsatisfiable : Exception
        {
        }
    }
}
