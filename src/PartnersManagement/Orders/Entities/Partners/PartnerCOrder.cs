namespace PartnersManagement.Orders.Entities.Partners
{
    public class PartnerCOrder : Order
    {
        public PartnerCOrder()
        {
            Partner = PartnerType.PartnerC;
        }

        public long ExposureId { get; init; }
        public string UDAC { get; init; }
        public string RelatedOrder { get; init; }
    }
}