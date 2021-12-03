using System;
using System.Collections.Generic;
using AutoMapper;
using PartnersManagement.Orders.Dtos;
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Entities.Partners;
using PartnersManagement.UnitTests.Common;
using Xunit;

namespace PartnersManagement.UnitTests.Orders
{
    public class OrderMappingsTests: IClassFixture<MappingFixture>
    {
        private readonly IMapper _mapper;

        public OrderMappingsTests(MappingFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _mapper.ConfigurationProvider
                .AssertConfigurationIsValid();
        }

        [Theory, MemberData(nameof(Data))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination,
            params object[] parameters)
        {
            var instance = Activator.CreateInstance(source, parameters);

            _mapper.Map(instance, source, destination);
        }

        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[]
                {
                    // these types will instantiate with reflection in the future
                    typeof(PartnerAOrder), typeof(OrderDto)
                };
                yield return new object[]
                {
                    typeof(PartnerBOrder), typeof(OrderDto)
                };
                yield return new object[]
                {
                    typeof(PartnerCOrder), typeof(OrderDto)
                };
                yield return new object[]
                {
                    typeof(PartnerDOrder), typeof(OrderDto)
                };

                yield return new object[]
                {
                    typeof(OrderDto), typeof(Order)
                };

                yield return new object[]
                {
                    typeof(OrderItemDto), typeof(OrderItem)
                };
                yield return new object[]
                {
                    typeof(PaidSearchProductOrderItem), typeof(OrderItemDto)
                };
                yield return new object[]
                {
                    typeof(WebSiteProductOrderItem), typeof(OrderItemDto)
                };

            }
        }
    }
}