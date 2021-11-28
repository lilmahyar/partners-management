using BuildingBlocks.Domain;

namespace PartnersManagement.Orders.Features.FetchOrderById
{
    public class FetchOrderByIdQuery : IQuery<FetchOrderByIdQueryResult>
    {
        public FetchOrderByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}