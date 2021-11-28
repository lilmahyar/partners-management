namespace PartnersManagement.Orders.Entities
{
    public class PaidSearchProductOrderItem : OrderItem
    {
        public PaidSearchProductOrderItem()
        {
            ProductType = ProductType.PaidProduct;
        }

        public AdWordCampaign AdWordCampaign { get; init; }
    }
}