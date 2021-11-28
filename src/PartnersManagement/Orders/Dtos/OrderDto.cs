using System.Collections.Generic;

namespace PartnersManagement.Orders.Dtos
{
    public class OrderDto
    {
        public PartnerType Partner { get; init; }
        public long OrderId { get; init; }
        public string TypeOfOder { get; init; }
        public string SubmittedBy { get; init; }
        public string CompanyId { get; init; }
        public string CompanyName { get; init; }
        public string ContactFirstName { get; init; }
        public string ContactLastName { get; init; }
        public string ContactTitle { get; init; }
        public string ContactPhone { get; init; }
        public string ContactMobile { get; init; }
        public string ContactEmail { get; init; }
        public string TypeOfOrder { get; init; }
        public long ExposureId { get; init; }
        public string UDAC { get; init; }
        public string RelatedOrder { get; init; }
        public IEnumerable<OrderItemDto> OrderItems { get; init; }
    }
}