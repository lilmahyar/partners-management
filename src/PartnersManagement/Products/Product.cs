using System;
using BuildingBlocks.Domain;

namespace PartnersManagement.Products
{
    public abstract class Product : IAggregate<Guid>
    {
        public Guid Id { get; init; }
        public ProductType ProductType { get; init; }
        public string Category { get; init;}
        public string Notes { get; init;}
    }


    public class PaidSearchProduct : Product
    {
        public PaidSearchProduct()
        {
            ProductType = ProductType.PaidProduct;
        }

        public AdWordCampaign AdWordCampaign { get; init; }
    }

    public class WebSiteProduct : Product
    {
        public WebSiteProduct()
        {
            ProductType = ProductType.WebSite;
        }

        public WebsiteDetails WebsiteDetails { get; init; }
    }
}