using System;
using BuildingBlocks.Domain;

namespace PartnersManagement.Partners
{
    public class Partner : IAggregate<Guid>
    {
        public Guid Id { get; init; }
        public string PartnerType { get; init; }
    }
}