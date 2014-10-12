using System;
using System.Collections.Generic;
using System.Linq;
using Ascensor.Machinery.Lang;

namespace Ascensor.Machinery
{
    public class Machine<TState, TInput>
        where TState : NonDeterministicFiniteStateMachine<TState, TInput>, new()
    {
        private readonly Stack<TState> _targets;
        private TState _target;

        public Machine(params TState[] targets)
        {
            if (targets.Length == 0) throw new ArgumentException("requires at most one target");

            _targets = new Stack<TState>(targets);
            _target = _targets.Pop();
        }

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
            var initial = new TState().InitialStates;

            // [gotcha]: have to use .Equals rather than == as the compiler can't
            // seem to figure out TState is going to be a reference type, despite
            // the constraint on NonDeterministicFiniteStateMachine
            var satisfying = initial.FirstOrDefault(state => state.Equals(_target));

            if (satisfying != null)
            {
                if (_targets.Count == 0)
                    return Enumerables.Of(Enumerables.Of(satisfying));
                
                _target = _targets.Pop();
            }

            maximumSteps--;
            if (maximumSteps == 0) throw new Machine.Unsatisfiable();

            // apply transitions for all valid inputs
            // to get a new set of states.
            //
            // rinse and repeat?
            return Enumerables.Of(Enumerable.Empty<TState>());
        }
    }

    public static class Machine
    {
        public class Unsatisfiable : Exception
        {
        }
    }
}
