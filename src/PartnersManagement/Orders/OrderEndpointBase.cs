using BuildingBlocks.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PartnersManagement.Orders
{
    [ApiExplorerSettings(GroupName = "Order Endpoints")]
    [ApiVersion("1.0")]
    [Authorize]
    [Route(BaseApiPath + "/orders")]
    public abstract class OrderEndpointBase : BaseController
    {
    }
}