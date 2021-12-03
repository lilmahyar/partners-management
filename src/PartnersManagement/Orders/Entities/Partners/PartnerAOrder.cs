namespace PartnersManagement.Orders.Entities.Partners
{
    public class PartnerAOrder : Order
    {
        public PartnerAOrder()
        {
             Partner = PartnerType.PartnerA;
        }

        public string ContactFirstName { get; init; }
        public string ContactLastName { get; init; }
        public string ContactTitle { get; init; }
        public string ContactPhone { get; init; }
        public string ContactMobile { get; init; }
        public string ContactEmail { get; init; }
    }
}