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
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Features.CreateOrder;
using PartnersManagement.Orders.Features.CreateOrder.Requests;
using Trill.Shared.Tests.Integration;
using Xunit;

namespace PartnersManagement.EndToEndTests.Orders
{
    public class CreateOrderTests : WebApiTestFixture<Startup, Data.PartnerManagementDbContext>
    {

        private Task<HttpResponseMessage> Act(CreateOrderRequest request)
        {
            JsonContent jsonContent = JsonContent.Create(request);
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"api/v1/orders", UriKind.RelativeOrAbsolute),
                Headers = { { ApiKeyConstants.HeaderName, "C5BFF7F0-B4DF-475E-A331-F737424F013C" } },
                Content = jsonContent
            };
            return Client.SendAsync(httpRequestMessage);
        }

        [Fact]
        public async Task create_order_endpoint_should_return_http_status_code_created_for_not_existing_order()
        {
            CreateOrderRequest request = OrderMocks.PartnerD_OrderDto;

            var response = await Act(request);
            var result = await response.Content.ReadFromJsonAsync<CreateOrderCommandResult>();

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task create_order_endpoint_should_return_location_header_with_correct_order_id()
        {
            CreateOrderRequest request = OrderMocks.PartnerD_OrderDto;

            var response = await Act(request);
            var result = await response.Content.ReadFromJsonAsync<CreateOrderCommandResult>();

            var locationHeader = response.Headers.FirstOrDefault(h => h.Key == "Location").Value.First();

            locationHeader.Should().NotBeNullOrEmpty();
            locationHeader.Should().Be($"{Client.BaseAddress}api/v1/orders/{result?.Id}");
        }

        [Fact]
        public async Task create_word_endpoint_should_persist_new_order_and_return_correct_id()
        {
            CreateOrderRequest request = OrderMocks.PartnerD_OrderDto;

            var response = await Act(request);
            var result = await response.Content.ReadFromJsonAsync<CreateOrderCommandResult>();

            var order = await FindAsync<Order>(result.Id);

            order.Should().NotBeNull();
            order.Id.Should().Be(result.Id);
        }
    }
}