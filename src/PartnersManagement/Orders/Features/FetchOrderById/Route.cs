using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PartnersManagement.Orders.Features.FetchOrderById
{
    [ApiVersion("1.0")]
    [Route(BaseApiPath + "/[controller]")]
    public partial class OrdersController : BaseController
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
        [Authorize]
        public async Task<ActionResult> FindOrderByIdAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new FetchOrderByIdQuery(id), cancellationToken);

            return Ok(result);
        }
    }
}