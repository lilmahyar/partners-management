using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PartnersManagement.Orders.Features.CreateOrder
{
    [ApiVersion("1.0")]
    [Route(BaseApiPath + "/[controller]")]
    public partial class OrdersController : BaseController
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(Summary = "Create a new Order",Description = "Create a new Order")]
        [Authorize]
        public async Task<ActionResult> CreateOrderAsync(CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            var command = Mapper.Map<CreateOrderCommand>(request);
            var result = await Mediator.Send(command, cancellationToken);

            return CreatedAtRoute("FindOrderByIdAsync", new { result.Id }, result);
        }
    }
}