using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartnersManagement.Orders.Features.CreateOrder.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace PartnersManagement.Orders.Features.CreateOrder
{
    public class CreateOrderEndpoint : OrderEndpointBase
    {
        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create a new Order", Description = "Create a new Order")]
        public async Task<ActionResult> CreateOrderAsync(CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            var command = Mapper.Map<CreateOrderCommand>(request);
            var result = await Mediator.Send(command, cancellationToken);

            return CreatedAtRoute("FindOrderByIdAsync", new { result.Id }, result);
        }
    }
}