using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using PartnersManagement.Orders.Exceptions;
using PartnersManagement.Orders.Features.CreateOrder;
using PartnersManagement.UnitTests.Common;
using PartnersManagement.UnitTests.Mocks;
using Xunit;

namespace PartnersManagement.UnitTests.Orders.Features.CreateOrder
{
    public class CreateOrderCommandHandlerTests : UnitTestFixture
    {
        private readonly CreateOrderCommandHandler _handler;

        private Task<CreateOrderCommandResult> Act(CreateOrderCommand command, CancellationToken cancellationToken) =>
            _handler.Handle(command, cancellationToken);

        public CreateOrderCommandHandlerTests()
        {
            // Arrange
            _handler = new CreateOrderCommandHandler(DbContext, Mapper);
        }

        [Fact]
        public async Task handler_with_valid_command_should_persist_new_order_and_return_correct_id()
        {
            // Arrange
            var command = new CreateOrderCommand(OrderMocks.PartnerA_OrderDto);

            //Act
            var result = await Act(command, CancellationToken.None);

            // Assert
            var entity = await DbContext.Orders.FindAsync(result.Id);

            entity.Should().NotBeNull();
            result.Id.Should().Be(entity.Id);
        }

        [Fact]
        public async Task handler_with_existing_order_id_command_should_throw_order_already_exists_exception()
        {
            // Arrange
            var command = new CreateOrderCommand(OrderMocks.PartnerA_OrderDto);
            await Act(command, CancellationToken.None);

            //Act && Assert
            Func<Task> act = async () => { await Act(command, CancellationToken.None); };
            await act.Should().ThrowAsync<OrderAlreadyExistsException>();
        }

        [Fact]
        public async Task handler_with_null_command_should_throw_argument_exception()
        {
            //Act && Assert
            Func<Task> act = async () => { await Act(null, CancellationToken.None); };
            await act.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}