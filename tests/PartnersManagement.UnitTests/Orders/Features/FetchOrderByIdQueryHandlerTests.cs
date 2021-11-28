using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using PartnersManagement.Orders;
using PartnersManagement.Orders.Dtos;
using PartnersManagement.Orders.Exceptions;
using PartnersManagement.Orders.Features.FetchOrderById;
using PartnersManagement.UnitTests.Common;
using Xunit;

namespace PartnersManagement.UnitTests.Orders.Features
{
    public class FetchOrderByIdQueryHandlerTests: UnitTestFixture
    {
        private readonly FetchOrderByIdQueryHandler _handler;

        private Task<FetchOrderByIdQueryResult> Act(FetchOrderByIdQuery query, CancellationToken cancellationToken) =>
            _handler.Handle(query, cancellationToken);

        public FetchOrderByIdQueryHandlerTests()
        {
            // Arrange
            _handler = new FetchOrderByIdQueryHandler(DbContext, Mapper);
        }

        [Fact]
        public async Task handle_with_invalid_order_by_id_query_should_throw_order_not_found_exception()
        {
            var query = new FetchOrderByIdQuery(2);

            //Act && Assert
            Func<Task> act = async () =>
            {
                await Act(query, CancellationToken.None);
            };
            await act.Should().ThrowAsync<OrderNotFoundException>();
        }

        [Fact]
        public async Task handler_with_null_query_should_throw_argument_exception()
        {
            //Act && Assert
            Func<Task> act = async () => { await Act(null, CancellationToken.None); };
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task handle_with_valid_movie_by_id_query_should_return_correct_order_dto()
        {
            // Arrange
            var query = new FetchOrderByIdQuery(1);

            var result = await Act(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeOfType<FetchOrderByIdQueryResult>();
            result.Order.Should().NotBeNull();
            result.Order.OrderId.Should().Be(1);
            result.Order.OrderItems.Count().Should().Be(1);
            result.Order.OrderItems.First().WebsiteDetails.Should().NotBeNull();
            result.Order.OrderItems.First().ProductType.Should().Be(ProductType.WebSite);
        }

    }
}