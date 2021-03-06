﻿using FluentNHibernate.Mapping;
using Snail.Adapters.Model;

namespace Snail.Adapters.Maps
{
    public class LocationMap : ClassMap<LocationModel>
    {
        public LocationMap()
        {
            Table("Locations");

            Id(x => x.Id);
            Map(x => x.CountryName);
            Map(x => x.PortName);
        }
    }
}
