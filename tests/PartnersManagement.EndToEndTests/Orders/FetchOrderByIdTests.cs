using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BuildingBlocks.Security.ApiKey;
using FluentAssertions;
using PartnersManagement.Api;
using PartnersManagement.EndToEndTests.Mocks;
using PartnersManagement.Orders.Features.FetchOrderById;
using Trill.Shared.Tests.Integration;
using Xunit;

namespace PartnersManagement.EndToEndTests.Orders
{
    public class FetchOrderByIdTests : WebApiTestFixture<Startup, Data.PartnerManagementDbContext>
    {
        private Task<HttpResponseMessage> Act(long orderId)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"api/v1/orders/{orderId}", UriKind.RelativeOrAbsolute),
                Headers = { { ApiKeyConstants.HeaderName, "C5BFF7F0-B4DF-475E-A331-F737424F013C" } }
            };
            return Client.SendAsync(httpRequestMessage);
        }

        [Fact]
        public async Task find_order_by_id_endpoint_should_return_http_status_code_ok()
        {
            // Arrange
            var existingOrder = OrderMocks.PartnerA_Order;
            await InsertAsync(existingOrder);

            // Act
            var response = await Act(existingOrder.Id);

            // Assert
            response.IsSuccessStatusCode.Should().Be(true);
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task find_order_by_id_endpoint_should_return_correct_data()
        {
            // Arrange
            var existingOrder = OrderMocks.PartnerA_Order;
            await InsertAsync(existingOrder);

            // Act
            var response = await Act(existingOrder.Id);
            var result = await response.Content.ReadFromJsonAsync<FetchOrderByIdQueryResult>();

            // Assert
            response.IsSuccessStatusCode.Should().Be(true);
            result.Should().NotBeNull();
            result.Should().BeOfType<FetchOrderByIdQueryResult>();
            result.Order.Should().NotBeNull();
            result.Order.OrderId.Should().Be(existingOrder.Id);
            result.Order.OrderItems.Any().Should().BeTrue();
            result.Order.OrderItems.Count().Should().Be(existingOrder.OrderItems.Count());
        }

        [Fact]
        public async Task find_order_by_id_endpoint_should_return_unauthorized_without_an_api_key()
        {
            var response = await Client.GetAsync("api/v1/orders/100");

            response.IsSuccessStatusCode.Should().Be(false);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}