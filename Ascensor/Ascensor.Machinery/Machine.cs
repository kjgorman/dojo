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
        private readonly TState _start;
        private readonly TState _goal;

        public Machine(params TState[] targets)
        {
            if (targets.Length == 0) throw new ArgumentException("requires at most one target");

            _targets = new Queue<TState>(targets);
            _goal = _targets.Last();
            _start = _targets.Dequeue();
        }

        private static IEnumerable<TState> Initial { get { return new TState().InitialStates; } }
        private static IEnumerable<Transition<TState, TInput>> Transitions { get { return new TState().Transitions; } }
        private static IEnumerable<TInput> Inputs { get { return new TState().Inputs; } } 

        public List<TState> RunToCompletion(int maximumSteps, bool shortest = false)
        {
            // TODO: implement this properly
            // should return the first path to complete for your exploration technique,
            // or throws an unsatisfiable exception while forcing the enumerable.
            var paths = Run(maximumSteps);

            return (shortest
                ? paths.OrderBy(path => path.Count()).First()
                : paths.First()).ToList();
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

            var successful = Initial
                .SelectMany(state => RunFrom(state, _start, _targets, maximumSteps))
                .Where(path => _goal.Equals(path.Last())).ToList();

            if (false == successful.Any()) throw new Machine.Unsatisfiable();

            return successful;
        }

        private IEnumerable<IEnumerable<TState>> RunFrom(TState state
                                                       , TState target
                                                       , Queue<TState> targets 
                                                       , int maximumSteps)
        {
            if (maximumSteps == 0) return Stuck();

            var current = Enumerables.Of(state);
            if (state.Equals(target))
            {
                if (targets.Count == 0) return Enumerables.Of(current);

                targets = new Queue<TState>(targets);
                target = targets.Dequeue();
                return RunFrom(state, target, targets, maximumSteps).Select(current.Concat);
            }

            var states = Inputs.SelectMany(input => Transitions
                                            .Where(t => t.Applicable(state, input))
                                            .Select(t => t.Traverse(state, input)))
                                            .ToList();

            return states.SelectMany(next => RunFrom(next, target, targets, maximumSteps - 1)).Select(current.Concat);
        }

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
