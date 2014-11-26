namespace Snail.Adapters.Model
{
    public class LocationModel
    {
        public virtual long Id { get; protected set; }
        public virtual string CountryName { get; set; }
        public virtual string PortName { get; set; }
    }
}
