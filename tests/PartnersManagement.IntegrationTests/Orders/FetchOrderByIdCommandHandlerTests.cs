using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Persistence;
using BuildingBlocks.Test.Fixtures;
using BuildingBlocks.Web;
using FluentAssertions;
using PartnersManagement.Api;
using PartnersManagement.Data;
using PartnersManagement.IntegrationTests.Mocks;
using PartnersManagement.Orders.Features.FetchOrderById;
using Xunit;

namespace PartnersManagement.IntegrationTests.Orders
{
    public class FetchOrderByIdCommandHandlerTests : IntegrationTestFixture<Startup, PartnerManagementDbContext>
    {
        public FetchOrderByIdCommandHandlerTests()
        {
            //setup the swaps for our tests
            RegisterTestServices(services => { services.ReplaceScoped<IDataSeeder, IntegrationTestDataSeeder>(); });
        }

        [Fact]
        public async Task find_order_by_id_query_should_return_a_valid_order_dto()
        {
            var existingOrder = OrderMocks.PartnerA_Order;
            await InsertAsync(existingOrder);

            // Arrange
            var query = new FetchOrderByIdQuery(existingOrder.Id);

            // Act
            var order = (await QueryAsync(query, CancellationToken.None)).Order;

            // Assert
            order.Should().NotBeNull();
            order.OrderId.Should().Be(existingOrder.Id);
            order.OrderItems.Any().Should().BeTrue();
            order.OrderItems.Count().Should().Be(existingOrder.OrderItems.Count());
        }
    }
}