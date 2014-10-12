using System;
using System.Collections.Generic;

namespace Ascensor.Machinery
{
    public abstract class NonDeterministicFiniteStateMachine<TState, TInput>
        where TState : class
    {
        public abstract HashSet<TState> InitialStates { get; }
        public abstract HashSet<TInput> Inputs { get; }
        public abstract HashSet<Func<TState, TInput, TState>> Transitions { get; }
        public abstract HashSet<TState> States { get; } 
    }
}
