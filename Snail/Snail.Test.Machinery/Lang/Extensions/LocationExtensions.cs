using Snail.Core.Shipping.Routes;

namespace Snail.Test.Machinery.Lang.Extensions
{
    public static class LocationExtensions
    {
        public static Location SomewhereElse(this Location location)
        {
            return new Location(location.LocationId + 1, string.Empty, string.Empty);
        }
    }
}
