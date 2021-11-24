using System;
using BuildingBlocks.Domain;

namespace PartnersManagement.Orders
{
    public class Order : IAggregate<Guid>
    {
        public Guid Id { get; init; }
    }
}