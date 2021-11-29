using System.Collections.Generic;
using System.Linq;
using PartnersManagement.Orders.Dtos;

namespace PartnersManagement.Orders.Features.CreateOrder.Requests
{
    public class CreateOrderRequest
    {
        public PartnerType Partner { get; set; }
        public string SubmittedBy { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactTitle { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public string TypeOfOrder { get; set; }
        public long ExposureId { get; set; }
        public string UDAC { get; set; }
        public string RelatedOrder { get; set; }
        public IEnumerable<OrderItemRequest> OrderItems { get; set; }

        public static implicit operator CreateOrderRequest(OrderDto orderDto)
        {
            return new CreateOrderRequest
            {
                Partner = orderDto.Partner,
                CompanyId = orderDto.CompanyId,
                CompanyName = orderDto.CompanyName,
                ContactEmail = orderDto.ContactEmail,
                ContactMobile = orderDto.ContactMobile,
                ContactPhone = orderDto.ContactPhone,
                ContactTitle = orderDto.ContactTitle,
                ExposureId = orderDto.ExposureId,
                RelatedOrder = orderDto.RelatedOrder,
                SubmittedBy = orderDto.SubmittedBy,
                ContactFirstName = orderDto.ContactFirstName,
                ContactLastName = orderDto.ContactLastName,
                TypeOfOrder = orderDto.TypeOfOrder,
                UDAC = orderDto.UDAC,
                OrderItems = orderDto.OrderItems.Select(x => new OrderItemRequest
                {
                    Category = x.Category,
                    Notes = x.Notes,
                    ProductId = x.ProductId,
                    ProductType = x.ProductType,
                    WebsiteDetails = x.WebsiteDetails,
                    AdWordCampaign = x.AdWordCampaign
                }),
            };
        }
    }
}