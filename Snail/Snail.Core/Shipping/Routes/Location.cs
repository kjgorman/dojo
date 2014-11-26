using System;

namespace Snail.Core.Shipping.Routes
{
    [Serializable]
    public class Location
    {
        public readonly long LocationId;
        private readonly string _portName;
        private readonly string _countryName;

        public Location(long locationId
                      , string countryName
                      , string portName)
        {
            LocationId = locationId;

            _countryName = countryName;
            _portName = portName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)        return false;
            if (!(obj is Location)) return false;

            var otherLocation = obj as Location;

            return LocationId == otherLocation.LocationId;
        }

        public override int GetHashCode()
        {
            unchecked { return (int) (LocationId % int.MaxValue); }
        }
    }
}
