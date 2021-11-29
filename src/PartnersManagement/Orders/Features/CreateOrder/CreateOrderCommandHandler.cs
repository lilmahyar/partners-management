using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using PartnersManagement.Data;
using PartnersManagement.Orders.Entities;

namespace PartnersManagement.Orders.Features.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResult>
    {
        private readonly PartnerManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(PartnerManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand command,
            CancellationToken cancellationToken)
        {
            Guard.Against.Null(command, nameof(CreateOrderCommand));

            var order = _mapper.Map<Order>(command);

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderCommandResult(order.Id);
        }
    }
}