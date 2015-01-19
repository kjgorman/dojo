using FluentNHibernate.Mapping;
using Snail.Adapters.Model;

namespace Snail.Adapters.Maps
{
    public class DocumentMap : ClassMap<DocumentModel>
    {
        public DocumentMap()
        {
            Table("Documents");

            Id(d => d.Id);
            Map(x => x.Amount);
            Map(x => x.CustomerAgreement);
            Map(x => x.Type).Column("[DocumentType]");
            Map(x => x.IssueDate);
            Map(x => x.ReceiptDate);
            Map(x => x.ShipmentDate);
        }
    }
}
