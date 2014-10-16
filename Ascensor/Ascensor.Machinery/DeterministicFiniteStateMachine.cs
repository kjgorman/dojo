using System.Collections.Generic;

namespace Ascensor.Machinery
{
    public abstract class DeterministicFiniteStateMachine<TState, TInput>
        where TState : class
    {
        public abstract HashSet<TState> InitialStates { get; }
        public abstract HashSet<TInput> Inputs { get; }
        public abstract HashSet<Transition<TState, TInput>> Transitions { get; }
        public abstract HashSet<TState> States { get; }
    }
}
