using System.Collections.Generic;
using PartnersManagement.Orders;
using PartnersManagement.Orders.Dtos;
using PartnersManagement.Orders.Entities;

namespace PartnersManagement.UnitTests.Mocks
{
    public static class OrderMocks
    {
        public static OrderDto PartnerA_OrderDto => new()
        {
            Partner = PartnerType.PartnerA,
            CompanyId = "123",
            CompanyName = "CompanyA",
            ContactEmail = "test@yahoo.com",
            ContactMobile = "5454545",
            ContactFirstName = "first name test",
            ContactLastName = "last name test",
            ContactPhone = "545454545",
            ContactTitle = "contact title test",
            TypeOfOrder = "order type test",
            SubmittedBy = "submit by test",
            OrderItems = new List<OrderItemDto>
            {
                new OrderItemDto
                {
                    Category = "category test",
                    Notes = "notes test",
                    ProductId = "127",
                    ProductType = ProductType.WebSite,
                    WebsiteDetails = new WebsiteDetailsDto
                    {
                        TemplateId = "12",
                        WebsiteCity = "city test",
                        WebsiteEmail = "web site email test",
                        WebsiteMobile = "web site mobile test",
                        WebsitePhone = "web site phone test",
                        WebsiteState = "web site status",
                        WebsiteAddressLine1 = "",
                        WebsiteAddressLine2 = "",
                        WebsiteBusinessName = "business name",
                        WebsitePostCode = "44444"
                    }
                }
            }
        };

        public static OrderDto PartnerD_OrderDto => new()
        {
            Partner = PartnerType.PartnerD,
            CompanyId = "123",
            CompanyName = "CompanyA",
            ContactEmail = "test@yahoo.com",
            ContactMobile = "5454545",
            ContactFirstName = "first name test",
            ContactLastName = "last name test",
            ContactPhone = "545454545",
            ContactTitle = "contact title test",
            TypeOfOrder = "order type test",
            SubmittedBy = "submit by test",
            OrderItems = new List<OrderItemDto>
            {
                new()
                {
                    ProductType = ProductType.PaidProduct,
                    Category = "test category",
                    ProductId = "12",
                    Notes = "notes test",
                    AdWordCampaign = new AdWordCampaignDto
                    {
                        Offer = "offer test",
                        CampaignName = "campaign name",
                        CampaignRadius = "campaign radius",
                        CampaignAddressLine1 = "",
                        CampaignPostCode = "post code",
                        LeadPhoneNumber = "333333333",
                        DestinationURL = "",
                        SMSPhoneNumber = "022222222"
                    }
                }
            }
        };
    }
}