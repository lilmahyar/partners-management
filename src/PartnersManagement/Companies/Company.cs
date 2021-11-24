using System;
using BuildingBlocks.Domain;

namespace PartnersManagement.Companies
{
    public class Company : IAggregate<Guid>
    {
        public Guid Id { get; init; }
    }
}