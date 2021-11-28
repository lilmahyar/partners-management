using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PartnersManagement.Data;
using PartnersManagement.Orders.Dtos;
using PartnersManagement.Orders.Exceptions;

namespace PartnersManagement.Orders.Features.FetchOrderById
{
    public class FetchOrderByIdQueryHandler : IRequestHandler<FetchOrderByIdQuery, FetchOrderByIdQueryResult>
    {
        private readonly PartnerManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public FetchOrderByIdQueryHandler(PartnerManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<FetchOrderByIdQueryResult> Handle(FetchOrderByIdQuery query,
            CancellationToken cancellationToken)
        {
            Guard.Against.Null(query, nameof(FetchOrderByIdQuery));

            var existOrder = await _dbContext.Orders.Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            if (existOrder is null)
                throw new OrderNotFoundException(query.Id);

            var orderDto = _mapper.Map<OrderDto>(existOrder);

            return new FetchOrderByIdQueryResult(orderDto);
        }
    }
}