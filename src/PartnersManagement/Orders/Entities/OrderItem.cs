namespace PartnersManagement.Orders.Entities
{
    public abstract class OrderItem
    {
        public long Id { get; init; }
        public string ProductId { get; init; }
        public ProductType ProductType { get; init; }
        public virtual Order Order { get; init; }
        public string Notes { get; init; }
        public string Category { get; init; }
    }
}