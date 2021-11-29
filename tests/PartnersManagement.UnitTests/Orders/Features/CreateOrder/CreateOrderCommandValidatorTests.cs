using System.Collections.Generic;
using FluentAssertions;
using FluentValidation.TestHelper;
using PartnersManagement.Orders;
using PartnersManagement.Orders.Dtos;
using PartnersManagement.Orders.Features.CreateOrder;
using PartnersManagement.UnitTests.Common;
using PartnersManagement.UnitTests.Mocks;
using Xunit;

namespace PartnersManagement.UnitTests.Orders.Features.CreateOrder
{
    public class CreateOrderCommandValidatorTests : UnitTestFixture
    {
        [Fact]
        public void is_valid_should_be_false_when_order_items_are_null_or_empty()
        {
            // Arrange
            var command = new CreateOrderCommand();
            var validator = new CreateOrderCommandValidator();

            var result = validator.TestValidate(command);
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.OrderItems);
        }

        [Fact]
        public void is_valid_should_be_true_when_partnerA_only_has_website_products()
        {
            // Arrange
            CreateOrderCommand command =  OrderMocks.PartnerA_OrderDto;
            var validator = new CreateOrderCommandValidator();

            var result = validator.TestValidate(command);
            result.IsValid.Should().BeTrue();
            result.ShouldNotHaveValidationErrorFor(x => x.OrderItems);
        }

        [Fact]
        public void is_valid_should_be_false_when_partnerA_has_both_website_and_paid_search_products()
        {
            // Arrange
            CreateOrderCommand command =  new CreateOrderCommand
            {
                Partner = PartnerType.PartnerA,
                OrderItems = new List<OrderItemDto>
                {
                    new()
                    {
                        WebsiteDetails = new WebsiteDetailsDto(),
                        AdWordCampaign = new AdWordCampaignDto()
                    }
                }
            };
            var validator = new CreateOrderCommandValidator();

            var result = validator.TestValidate(command);
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.OrderItems).WithErrorMessage("Partner A not support PaidSearch");
        }

        [Fact]
        public void is_valid_should_be_false_when_partnerA_has_no_website_product()
        {
            // Arrange
            CreateOrderCommand command = new CreateOrderCommand
            {
                Partner = PartnerType.PartnerA,
                OrderItems = new List<OrderItemDto>
                {
                    new()
                    {
                        WebsiteDetails = null
                    }
                }
            };
            var validator = new CreateOrderCommandValidator();

            var result = validator.TestValidate(command);
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.OrderItems).WithErrorMessage("Partner A missing WebSite Product");
        }

        [Fact]
        public void is_valid_should_be_false_when_partnerA_product_type_is_not_website()
        {
            // Arrange
            CreateOrderCommand command = new CreateOrderCommand
            {
                Partner = PartnerType.PartnerA,
                OrderItems = new List<OrderItemDto>
                {
                    new()
                    {
                        ProductType = ProductType.PaidProduct,
                        WebsiteDetails = new WebsiteDetailsDto()
                    }
                }
            };
            var validator = new CreateOrderCommandValidator();

            var result = validator.TestValidate(command);
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.OrderItems).WithErrorMessage("Partner A product type should be 'Website'");
        }

        [Fact]
        public void is_valid_should_be_false_when_there_is_a_conflict_between_product_type_and_its_details_property()
        {
            // Arrange
            CreateOrderCommand command = new CreateOrderCommand
            {
                Partner = PartnerType.PartnerC,
                OrderItems = new List<OrderItemDto>
                {
                    new()
                    {
                        ProductType = ProductType.PaidProduct,
                        WebsiteDetails = new WebsiteDetailsDto()
                    }
                }
            };
            var validator = new CreateOrderCommandValidator();

            var result = validator.TestValidate(command);
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.OrderItems).WithErrorMessage("Conflict between product type and its detail property");
        }
    }
}