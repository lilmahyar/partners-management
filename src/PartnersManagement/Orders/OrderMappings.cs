using AutoMapper;
using PartnersManagement.Orders.Dtos;
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Features.CreateOrder;

namespace PartnersManagement.Orders
{
    public class OrderMappings : Profile
    {
        public OrderMappings()
        {
            CreateMap<OrderDto, Order>()
                .ForMember(x => x.Partner, opt => opt.MapFrom(x => x.Partner))
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.OrderId))
                .ReverseMap();

            CreateMap<WebsiteDetails, WebsiteDetailsDto>().ReverseMap();
            CreateMap<AdWordCampaign, AdWordCampaignDto>().ReverseMap();

            CreateMap<OrderItemDto, OrderItem>()
                .IncludeAllDerived()
                .ConvertUsing<OrderItemDtoToOrderItemConvertor>();

            CreateMap<OrderItem, OrderItemDto>()
                .IncludeAllDerived()
                .ConvertUsing<OrderItemToOrderItemDtoConvertor>();

            CreateMap<CreateOrderRequest, CreateOrderCommand>().ConstructUsing(x => new CreateOrderCommand(x.Order));
        }
    }

    public class OrderItemToOrderItemDtoConvertor : ITypeConverter<OrderItem, OrderItemDto>
    {
        public OrderItemDto Convert(OrderItem orderItem, OrderItemDto orderItemDto, ResolutionContext context)
        {
            if (orderItem is WebSiteProductOrderItem siteItem)
            {
                return new OrderItemDto
                {
                    Id = siteItem.Id,
                    ProductId = siteItem.ProductId,
                    ProductType = siteItem.ProductType,
                    Notes = siteItem.Notes,
                    Category = siteItem.Category,
                    WebsiteDetails = siteItem.WebsiteDetails == null
                        ? null
                        : context.Mapper.Map<WebsiteDetailsDto>(siteItem.WebsiteDetails)
                };
            }

            if (orderItem is PaidSearchProductOrderItem paidItem)
            {
                return new OrderItemDto
                {
                    Id = paidItem.Id,
                    ProductId = paidItem.ProductId,
                    ProductType = paidItem.ProductType,
                    Notes = paidItem.Notes,
                    Category = paidItem.Category,
                    AdWordCampaign = paidItem.AdWordCampaign == null
                        ? null
                        : context.Mapper.Map<AdWordCampaignDto>(paidItem.AdWordCampaign)
                };
            }

            return null;
        }
    }


    public class OrderItemDtoToOrderItemConvertor : ITypeConverter<OrderItemDto, OrderItem>
    {
        public OrderItem Convert(OrderItemDto orderItemDto, OrderItem destination, ResolutionContext context)
        {
            if (orderItemDto.WebsiteDetails != null)
            {
                return new WebSiteProductOrderItem
                {
                    Id = orderItemDto.Id,
                    ProductId = orderItemDto.ProductId,
                    ProductType = orderItemDto.ProductType,
                    Notes = orderItemDto.Notes,
                    Category = orderItemDto.Category,
                    WebsiteDetails = new WebsiteDetails()
                    {
                        TemplateId = orderItemDto.WebsiteDetails.TemplateId,
                        WebsiteBusinessName = orderItemDto.WebsiteDetails.WebsiteBusinessName,
                        WebsiteAddressLine1 = orderItemDto.WebsiteDetails.WebsiteAddressLine1,
                        WebsiteAddressLine2 = orderItemDto.WebsiteDetails.WebsiteAddressLine2,
                        WebsiteCity = orderItemDto.WebsiteDetails.WebsiteCity,
                        WebsiteState = orderItemDto.WebsiteDetails.WebsiteState,
                        WebsitePostCode = orderItemDto.WebsiteDetails.WebsitePostCode,
                        WebsitePhone = orderItemDto.WebsiteDetails.WebsitePhone,
                        WebsiteEmail = orderItemDto.WebsiteDetails.WebsiteEmail,
                        WebsiteMobile = orderItemDto.WebsiteDetails.WebsiteMobile,
                    }
                };
            }

            if (orderItemDto.AdWordCampaign != null)
            {
                return new PaidSearchProductOrderItem
                {
                    Id = orderItemDto.Id,
                    ProductId = orderItemDto.ProductId,
                    ProductType = orderItemDto.ProductType,
                    Notes = orderItemDto.Notes,
                    Category = orderItemDto.Category,
                    AdWordCampaign = new AdWordCampaign()
                    {
                        CampaignName = orderItemDto.AdWordCampaign.CampaignName,
                        CampaignAddressLine1 = orderItemDto.AdWordCampaign.CampaignAddressLine1,
                        CampaignPostCode = orderItemDto.AdWordCampaign.CampaignPostCode,
                        CampaignRadius = orderItemDto.AdWordCampaign.CampaignRadius,
                        LeadPhoneNumber = orderItemDto.AdWordCampaign.LeadPhoneNumber,
                        UniqueSellingPoint1 = orderItemDto.AdWordCampaign.UniqueSellingPoint1,
                        UniqueSellingPoint2 = orderItemDto.AdWordCampaign.UniqueSellingPoint2,
                        UniqueSellingPoint3 = orderItemDto.AdWordCampaign.UniqueSellingPoint3,
                        Offer = orderItemDto.AdWordCampaign.Offer,
                        DestinationURL = orderItemDto.AdWordCampaign.DestinationURL,
                    }
                };
            }

            return null;
        }
    }
}