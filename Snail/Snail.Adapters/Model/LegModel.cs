namespace Snail.Adapters.Model
{
    public class LegModel
    {
        public virtual long Id { get; protected set; }
        public virtual LocationModel From { get; protected set; }
        public virtual LocationModel To { get; protected set; }
    }
}
