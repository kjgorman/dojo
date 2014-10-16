namespace Ascensor.Machinery
{
    public abstract  class Transition<TState, TInput>
    {
        public abstract bool Applicable(TState state, TInput input);

        public abstract TState Traverse(TState state, TInput input);
    }
}
