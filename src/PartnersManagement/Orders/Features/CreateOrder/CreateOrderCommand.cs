using System.Collections.Generic;
using BuildingBlocks.Domain;
using PartnersManagement.Orders.Dtos;

namespace PartnersManagement.Orders.Features.CreateOrder
{
    public class CreateOrderCommand : ICommand<CreateOrderCommandResult>
    {
        public PartnerType Partner { get; init; }
        public string SubmittedBy { get; init; }
        public string CompanyId { get; init; }
        public string CompanyName { get; init; }
        public string ContactFirstName { get; init; }
        public string ContactLastName { get; init; }
        public string ContactTitle { get; init; }
        public string ContactPhone { get; init; }
        public string ContactMobile { get; init; }
        public string ContactEmail { get; init; }
        public string TypeOfOrder { get; init; }
        public long ExposureId { get; init; }
        public string UDAC { get; init; }
        public string RelatedOrder { get; init; }
        public IEnumerable<OrderItemDto> OrderItems { get; init; }

        public static implicit operator CreateOrderCommand(OrderDto orderDto)
        {
            return new CreateOrderCommand
            {
                Partner = orderDto.Partner,
                CompanyId = orderDto.CompanyId,
                CompanyName = orderDto.CompanyName,
                ContactEmail = orderDto.ContactEmail,
                ContactMobile = orderDto.ContactMobile,
                ContactPhone = orderDto.ContactPhone,
                ContactTitle = orderDto.ContactTitle,
                ExposureId = orderDto.ExposureId,
                RelatedOrder = orderDto.RelatedOrder,
                SubmittedBy = orderDto.SubmittedBy,
                ContactFirstName = orderDto.ContactFirstName,
                ContactLastName = orderDto.ContactLastName,
                TypeOfOrder = orderDto.TypeOfOrder,
                UDAC = orderDto.UDAC,
                OrderItems = orderDto.OrderItems,
            };
        }
    }
}