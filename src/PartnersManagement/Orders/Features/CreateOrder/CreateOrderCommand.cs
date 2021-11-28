using BuildingBlocks.Domain;
using PartnersManagement.Orders.Dtos;

namespace PartnersManagement.Orders.Features.CreateOrder
{
    public class CreateOrderCommand : ICommand<CreateOrderCommandResult>
    {
        public CreateOrderCommand(OrderDto orderDto)
        {
            OrderDto = orderDto;
        }

        public OrderDto OrderDto { get; }
    }
}