using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PartnersManagement.Orders.Features.FetchOrderById
{
    public class FindOrderByIdEndpoint : OrderEndpointBase
    {
        /// <summary>
        /// Get specific order by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:long}", Name = nameof(FindOrderByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get specific order by id", Description = "Get specific order by Id")]
        public async Task<ActionResult> FindOrderByIdAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new FetchOrderByIdQuery(id), cancellationToken);

            return Ok(result);
        }
    }
}