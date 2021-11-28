using BuildingBlocks.Exception;

namespace PartnersManagement.Orders.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(long id) : base($"order with id '{id}' not found in the database.")
        {
        }
    }
}