using System.Collections.Generic;
using BuildingBlocks.Domain;

namespace PartnersManagement.Orders.Entities
{
    public abstract class Order : IAggregate<long>
    {
        public long Id { get; init; }
        public PartnerType Partner { get; init; }
        public string TypeOfOrder { get; init; }
        public string SubmittedBy { get; init; }
        public string CompanyId { get; init; }
        public string CompanyName { get; init; }
        public IEnumerable<OrderItem> OrderItems { get; init; }
    }
}