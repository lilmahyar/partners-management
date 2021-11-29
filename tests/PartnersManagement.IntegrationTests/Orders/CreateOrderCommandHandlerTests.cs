using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Persistence;
using BuildingBlocks.Test.Fixtures;
using BuildingBlocks.Web;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PartnersManagement.Api;
using PartnersManagement.Data;
using PartnersManagement.IntegrationTests.Mocks;
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Features.CreateOrder;
using Xunit;

namespace PartnersManagement.IntegrationTests.Orders
{
    public class CreateOrderCommandHandlerTests : IntegrationTestFixture<Startup, PartnerManagementDbContext>
    {
        public CreateOrderCommandHandlerTests()
        {
            //setup the swaps for our tests
            RegisterTestServices(services => { services.ReplaceScoped<IDataSeeder, IntegrationTestDataSeeder>(); });
        }


        [Fact]
        public async Task valid_create_new_order_command_should_persist_new_order_and_return_correct_id()
        {
            // Arrange
            CreateOrderCommand command = OrderMocks.PartnerD_OrderDto;

            // Act
            var createdOrderResponse = await SendAsync(command, CancellationToken.None);

            // Assert
            Order createdOrder = await ExecuteDbContextAsync(db =>
            {
                return db.Orders.Include(x => x.OrderItems)
                    .SingleOrDefaultAsync(x => x.Id == createdOrderResponse.Id);
            });

            createdOrder.Should().NotBeNull();
            createdOrder.Id.Should().Be(createdOrderResponse.Id);
            createdOrder.OrderItems.Any().Should().BeTrue();
            createdOrder.OrderItems.Count().Should().Be(command.OrderItems.Count());
            createdOrder.OrderItems.First().Should().BeOfType<PaidSearchProductOrderItem>();
            createdOrder.OrderItems.First().ProductType.Should().Be(command.OrderItems.First().ProductType);
            (createdOrder.OrderItems.First() as PaidSearchProductOrderItem)?.AdWordCampaign.Should().NotBeNull();
        }
    }
}