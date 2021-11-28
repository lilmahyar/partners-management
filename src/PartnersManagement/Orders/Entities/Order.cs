using System.Collections.Generic;
using BuildingBlocks.Domain;

namespace PartnersManagement.Orders.Entities
{
    public class Order : IAggregate<long>
    {
        public long Id { get; init; }
        public PartnerType Partner { get; init; }
        public string TypeOfOrder { get; init; }
        public string SubmittedBy { get; init; }
        public string CompanyId { get; init; }
        public string CompanyName { get; init; }
        public IEnumerable<OrderItem> OrderItems { get; init; }

        #region Extra

        public string ContactFirstName { get; init; }
        public string ContactLastName { get; init; }
        public string ContactTitle { get; init; }
        public string ContactPhone { get; init; }
        public string ContactMobile { get; init; }
        public string ContactEmail { get; init; }
        public long ExposureId { get; init; }
        public string UDAC { get; init; }
        public string RelatedOrder { get; init; }

        #endregion
    }
}