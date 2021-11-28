using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BuildingBlocks.Security.ApiKey;
using FluentAssertions;
using NSubstitute;
using PartnersManagement.Api;
using PartnersManagement.EndToEndTests.Mocks;
using PartnersManagement.Orders;
using PartnersManagement.Orders.Dtos;
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Features.CreateOrder;
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
            var request = new CreateOrderRequest
            {
               Order = OrderMocks.PartnerD_OrderDto
            };

            var response = await Act(request);
            var result = await response.Content.ReadFromJsonAsync<CreateOrderCommandResult>();

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task create_order_endpoint_should_return_location_header_with_correct_order_id()
        {
            var request = new CreateOrderRequest
            {
                Order = OrderMocks.PartnerD_OrderDto
            };

            var response = await Act(request);
            var result = await response.Content.ReadFromJsonAsync<CreateOrderCommandResult>();

            var locationHeader = response.Headers.FirstOrDefault(h => h.Key == "Location").Value.First();

            locationHeader.Should().NotBeNullOrEmpty();
            locationHeader.Should().Be($"{Client.BaseAddress}api/v1/orders/{result?.Id}");
        }

        [Fact]
        public async Task create_word_endpoint_should_persist_new_order_and_return_correct_id()
        {
            var request = new CreateOrderRequest
            {
                Order = OrderMocks.PartnerD_OrderDto
            };

            var response = await Act(request);
            var result = await response.Content.ReadFromJsonAsync<CreateOrderCommandResult>();

            var order = await FindAsync<Order>(result.Id);

            order.Should().NotBeNull();
            order.Id.Should().Be(result.Id);
        }

        [Fact]
        public async Task create_endpoint_should_return_conflict_status_code_if_order_already_exist()
        {
            var request = new CreateOrderRequest
            {
                Order = OrderMocks.PartnerD_OrderDto
            };

            var response = await Act(request);
            var result = await response.Content.ReadFromJsonAsync<CreateOrderCommandResult>();

            var response2 = await Act(new CreateOrderRequest()
            {
                Order = new OrderDto
                {
                    OrderId = result.Id,
                    Partner = PartnerType.PartnerA,
                    CompanyId = "123",
                    CompanyName = "CompanyA",
                    ContactEmail = "test@yahoo.com",
                    ContactMobile = "5454545",
                    ContactFirstName = "first name test",
                    ContactLastName = "last name test",
                    ContactPhone = "545454545",
                    ContactTitle = "contact title test",
                    TypeOfOrder = "order type test",
                    SubmittedBy = "submit by test",
                    OrderItems = new List<OrderItemDto>
                    {
                        new OrderItemDto
                        {
                            Category = "category test",
                            Notes = "notes test",
                            ProductId = "127",
                            ProductType = ProductType.WebSite,
                            WebsiteDetails = new WebsiteDetailsDto
                            {
                                TemplateId = "12",
                                WebsiteCity = "city test",
                                WebsiteEmail = "web site email test",
                                WebsiteMobile = "web site mobile test",
                                WebsitePhone = "web site phone test",
                                WebsiteState = "web site status",
                                WebsiteAddressLine1 = "",
                                WebsiteAddressLine2 = "",
                                WebsiteBusinessName = "business name",
                                WebsitePostCode = "44444"
                            }
                        }
                    }
                }
            });

            response2.Should().NotBeNull();
            response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}