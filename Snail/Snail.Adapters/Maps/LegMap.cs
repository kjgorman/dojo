using FluentNHibernate.Mapping;
using Snail.Adapters.Model;

namespace Snail.Adapters.Maps
{
    public class LegMap : ClassMap<LegModel>
    {
        public LegMap()
        {
            Table("Legs");

            Id(l => l.Id);
            References(l => l.From).Column("[From]");
            References(l => l.To).Column("[To]");
        }
    }
}
