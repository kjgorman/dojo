using System;
using System.Collections.Generic;
using Snail.Core.Shipping.Routes;

namespace Snail.Test.Machinery.Lang
{
    public static class Legs
    {
        public static List<Leg> Between(Location origin, Location destination)
        {
            return new List<Leg> { new Leg(origin, destination, 1L) };
        }

        public static List<Leg> Then(this List<Leg> previous, Location next)
        {
            if (previous.Count == 0)
                throw new ArgumentException("#Then requires a location to come from!");

            var last = previous[previous.Count - 1];

            return new List<Leg>(previous) { new Leg(last.From, next, last.LegId + 1) };
        }
    }
}
