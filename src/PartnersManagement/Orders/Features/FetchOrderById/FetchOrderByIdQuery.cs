using System;
using BuildingBlocks.Caching;
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

        public class CachePolicy : ICachePolicy<FetchOrderByIdQuery, FetchOrderByIdQueryResult>
        {
            public DateTime? AbsoluteExpirationRelativeToNow => DateTime.Now.AddMinutes(15);

            public string GetCacheKey(FetchOrderByIdQuery query)
            {
                return CacheKey.With(query.GetType(), query.Id.ToString());
            }
        }
    }
}