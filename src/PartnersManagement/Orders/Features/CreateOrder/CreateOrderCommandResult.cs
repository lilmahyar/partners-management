namespace PartnersManagement.Orders.Features.CreateOrder
{
    public class CreateOrderCommandResult
    {
        public CreateOrderCommandResult(long id)
        {
            Id = id;
        }

        public long Id { get;  }
    }
}