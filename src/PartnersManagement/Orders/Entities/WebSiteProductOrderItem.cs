namespace PartnersManagement.Orders.Entities
{
    public class WebSiteProductOrderItem : OrderItem
    {
        public WebSiteProductOrderItem()
        {
            ProductType = ProductType.WebSite;
        }

        public WebsiteDetails WebsiteDetails { get; init; }
    }
}