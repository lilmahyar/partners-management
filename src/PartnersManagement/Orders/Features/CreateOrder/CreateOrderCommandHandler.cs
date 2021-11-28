using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using PartnersManagement.Data;
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Exceptions;

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

            try
            {
                var exists = _dbContext.Orders.Any(x => x.Id == command.OrderDto.OrderId);
                if (exists)
                    throw new OrderAlreadyExistsException(command.OrderDto.OrderId);

                var order = _mapper.Map<Order>(command.OrderDto);

                await _dbContext.Orders.AddAsync(order, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new CreateOrderCommandResult(order.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}