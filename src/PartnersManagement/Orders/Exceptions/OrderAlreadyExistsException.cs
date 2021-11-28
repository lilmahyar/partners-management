using BuildingBlocks.Exception;

namespace PartnersManagement.Orders.Exceptions
{
    public class OrderAlreadyExistsException : AppException
    {
        public OrderAlreadyExistsException(long id) : base($"order with id {id} already exist in the database.")
        {
        }
    }
}