using BuildingBlocks.Test.Fixtures;
using PartnersManagement.Api;
using PartnersManagement.Data;
using Xunit;

namespace PartnersManagement.IntegrationTests
{
    [CollectionDefinition(nameof(IntegrationTestSharedFixtureCollection))]
    public class IntegrationTestSharedFixtureCollection :
        ICollectionFixture<IntegrationTestFixture<Startup, PartnerManagementDbContext>>
    {
    }
}