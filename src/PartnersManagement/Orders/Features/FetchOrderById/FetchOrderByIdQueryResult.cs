using PartnersManagement.Orders.Dtos;

namespace PartnersManagement.Orders.Features.FetchOrderById
{
    public class FetchOrderByIdQueryResult
    {
        public FetchOrderByIdQueryResult(OrderDto order)
        {
            Order = order;
        }

        public OrderDto Order { get; }
    }
}