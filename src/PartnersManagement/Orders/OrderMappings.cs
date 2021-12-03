using System.Collections.Generic;
using AutoMapper;
using PartnersManagement.Orders.Dtos;
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Entities.Partners;
using PartnersManagement.Orders.Features.CreateOrder;
using PartnersManagement.Orders.Features.CreateOrder.Requests;

namespace PartnersManagement.Orders
{
    public class OrderMappings : Profile
    {
        public OrderMappings()
        {
            CreateMap<OrderDto, Order>()
                .ConvertUsing<OrderDtoToOrder>();

            CreateMap<Order, OrderDto>()
                .ConvertUsing<OrderToOrderDto>();

            CreateMap<WebsiteDetails, WebsiteDetailsDto>().ReverseMap();
            CreateMap<AdWordCampaign, AdWordCampaignDto>().ReverseMap();

            CreateMap<OrderItemDto, OrderItem>()
                .IncludeAllDerived()
                .ConvertUsing<OrderItemDtoToOrderItemConvertor>();

            CreateMap<OrderItem, OrderItemDto>()
                .IncludeAllDerived()
                .ConvertUsing<OrderItemToOrderItemDtoConvertor>();

            CreateMap<CreateOrderRequest, CreateOrderCommand>();

            CreateMap<CreateOrderCommand, Order>()
                .IncludeAllDerived()
                .ConvertUsing<CreateOrderCommandToOrder>();

            CreateMap<OrderItemRequest, OrderItemDto>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }

    public class OrderToOrderDto : ITypeConverter<Order, OrderDto>
    {
        public OrderDto Convert(Order order, OrderDto orderDto, ResolutionContext context)
        {
            if (order is PartnerAOrder partnerA)
            {
                return new OrderDto
                {
                    Partner = partnerA.Partner,
                    CompanyId = partnerA.CompanyId,
                    CompanyName = partnerA.CompanyName,
                    ContactEmail = partnerA.ContactEmail,
                    ContactMobile = partnerA.ContactMobile,
                    ContactPhone = partnerA.ContactPhone,
                    ContactTitle = partnerA.ContactTitle,
                    ContactFirstName = partnerA.ContactFirstName,
                    ContactLastName = partnerA.ContactLastName,
                    OrderId = partnerA.Id,
                    SubmittedBy = partnerA.SubmittedBy,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItemDto>>(partnerA.OrderItems)
                };
            }

            if (order is PartnerBOrder partnerB)
            {
                return new OrderDto
                {
                    Partner = partnerB.Partner,
                    CompanyId = partnerB.CompanyId,
                    CompanyName = partnerB.CompanyName,
                    OrderId = partnerB.Id,
                    SubmittedBy = partnerB.SubmittedBy,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItemDto>>(partnerB.OrderItems)
                };
            }

            if (order is PartnerCOrder partnerC)
            {
                return new OrderDto
                {
                    Partner = partnerC.Partner,
                    CompanyId = partnerC.CompanyId,
                    CompanyName = partnerC.CompanyName,
                    OrderId = partnerC.Id,
                    SubmittedBy = partnerC.SubmittedBy,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItemDto>>(partnerC.OrderItems),
                    TypeOfOrder = partnerC.TypeOfOrder,
                    UDAC = partnerC.UDAC,
                    ExposureId = partnerC.ExposureId,
                    RelatedOrder = partnerC.RelatedOrder
                };
            }

            if (order is PartnerDOrder partnerD)
            {
                return new OrderDto
                {
                    Partner = partnerD.Partner,
                    CompanyId = partnerD.CompanyId,
                    CompanyName = partnerD.CompanyName,
                    OrderId = partnerD.Id,
                    SubmittedBy = partnerD.SubmittedBy,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItemDto>>(partnerD.OrderItems)
                };
            }
            return null;
        }
    }

    public class OrderDtoToOrder : ITypeConverter<OrderDto, Order>
    {
        public Order Convert(OrderDto orderDto, Order order, ResolutionContext context)
        {
            if (orderDto.Partner == PartnerType.PartnerA)
                return new PartnerAOrder
                {
                    Id = orderDto.OrderId,
                    Partner = PartnerType.PartnerA,
                    CompanyId = orderDto.CompanyId,
                    CompanyName = orderDto.CompanyName,
                    ContactEmail = orderDto.ContactEmail,
                    ContactMobile = orderDto.ContactMobile,
                    ContactPhone = orderDto.ContactPhone,
                    ContactTitle = orderDto.ContactTitle,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItem>>(orderDto.OrderItems),
                    SubmittedBy = orderDto.SubmittedBy,
                    ContactFirstName = orderDto.ContactFirstName,
                    ContactLastName = orderDto.ContactLastName,
                    TypeOfOrder = orderDto.TypeOfOrder
                };
            if (orderDto.Partner == PartnerType.PartnerB)
                return new PartnerBOrder()
                {
                    Id = orderDto.OrderId,
                    Partner = PartnerType.PartnerA,
                    CompanyId = orderDto.CompanyId,
                    CompanyName = orderDto.CompanyName,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItem>>(orderDto.OrderItems),
                    SubmittedBy = orderDto.SubmittedBy,
                    TypeOfOrder = orderDto.TypeOfOrder,
                };
            if (orderDto.Partner == PartnerType.PartnerC)
                return new PartnerCOrder
                {
                    Id = orderDto.OrderId,
                    Partner = PartnerType.PartnerA,
                    CompanyId = orderDto.CompanyId,
                    CompanyName = orderDto.CompanyName,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItem>>(orderDto.OrderItems),
                    SubmittedBy = orderDto.SubmittedBy,
                    TypeOfOrder = orderDto.TypeOfOrder,
                    ExposureId = orderDto.ExposureId,
                    RelatedOrder = orderDto.RelatedOrder,
                    UDAC = orderDto.UDAC,
                };
            if (orderDto.Partner == PartnerType.PartnerD)
                return new PartnerDOrder
                {
                    Id = orderDto.OrderId,
                    Partner = PartnerType.PartnerA,
                    CompanyId = orderDto.CompanyId,
                    CompanyName = orderDto.CompanyName,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItem>>(orderDto.OrderItems),
                    SubmittedBy = orderDto.SubmittedBy,
                    TypeOfOrder = orderDto.TypeOfOrder,
                };

            return null;
        }
    }

    public class CreateOrderCommandToOrder : ITypeConverter<CreateOrderCommand, Order>
    {
        public Order Convert(CreateOrderCommand createOrderCommand, Order order, ResolutionContext context)
        {
            if (createOrderCommand.Partner == PartnerType.PartnerA)
                return new PartnerAOrder
                {
                    Partner = PartnerType.PartnerA,
                    CompanyId = createOrderCommand.CompanyId,
                    CompanyName = createOrderCommand.CompanyName,
                    ContactEmail = createOrderCommand.ContactEmail,
                    ContactMobile = createOrderCommand.ContactMobile,
                    ContactPhone = createOrderCommand.ContactPhone,
                    ContactTitle = createOrderCommand.ContactTitle,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItem>>(createOrderCommand.OrderItems),
                    SubmittedBy = createOrderCommand.SubmittedBy,
                    ContactFirstName = createOrderCommand.ContactFirstName,
                    ContactLastName = createOrderCommand.ContactLastName,
                    TypeOfOrder = createOrderCommand.TypeOfOrder
                };
            if (createOrderCommand.Partner == PartnerType.PartnerB)
                return new PartnerBOrder()
                {
                    Partner = PartnerType.PartnerA,
                    CompanyId = createOrderCommand.CompanyId,
                    CompanyName = createOrderCommand.CompanyName,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItem>>(createOrderCommand.OrderItems),
                    SubmittedBy = createOrderCommand.SubmittedBy,
                    TypeOfOrder = createOrderCommand.TypeOfOrder,
                };
            if (createOrderCommand.Partner == PartnerType.PartnerC)
                return new PartnerCOrder
                {
                    Partner = PartnerType.PartnerA,
                    CompanyId = createOrderCommand.CompanyId,
                    CompanyName = createOrderCommand.CompanyName,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItem>>(createOrderCommand.OrderItems),
                    SubmittedBy = createOrderCommand.SubmittedBy,
                    TypeOfOrder = createOrderCommand.TypeOfOrder,
                    ExposureId = createOrderCommand.ExposureId,
                    RelatedOrder = createOrderCommand.RelatedOrder,
                    UDAC = createOrderCommand.UDAC,
                };
            if (createOrderCommand.Partner == PartnerType.PartnerD)
                return new PartnerDOrder
                {
                    Partner = PartnerType.PartnerA,
                    CompanyId = createOrderCommand.CompanyId,
                    CompanyName = createOrderCommand.CompanyName,
                    OrderItems = context.Mapper.Map<IEnumerable<OrderItem>>(createOrderCommand.OrderItems),
                    SubmittedBy = createOrderCommand.SubmittedBy,
                    TypeOfOrder = createOrderCommand.TypeOfOrder,
                };

            return null;
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