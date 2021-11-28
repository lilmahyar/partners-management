namespace PartnersManagement.Orders.Dtos
{
    public class OrderItemDto
    {
        public long Id { get; init; }
        public string ProductId { get; init; }
        public ProductType ProductType { get; init; }
        public string Notes { get; init; }
        public string Category { get; init; }
        public AdWordCampaignDto AdWordCampaign { get; init; }
        public WebsiteDetailsDto WebsiteDetails { get; init; }
    }
}