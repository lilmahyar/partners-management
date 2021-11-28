using System.Linq;
using FluentValidation;

namespace PartnersManagement.Orders.Features.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.OrderDto).NotNull().WithMessage("OrderDto can't be null.");

            RuleFor(x => x.OrderDto.OrderItems).Cascade(CascadeMode.Stop).NotEmpty().NotNull();

            When(x => x.OrderDto.Partner == PartnerType.PartnerA,
                () =>
                {
                    RuleFor(x => x.OrderDto.OrderItems).Must(x => x.All(s => s.AdWordCampaign == null))
                        .WithMessage("Partner A not support PaidSearch");

                    RuleFor(x => x.OrderDto.OrderItems).Must(x => x.All(s => s.WebsiteDetails != null))
                        .WithMessage("Partner A missing WebSite Product");

                    RuleFor(x => x.OrderDto.OrderItems).Must(x => x.All(s => s.ProductType == ProductType.WebSite))
                        .WithMessage("Partner A product type should be 'Website'");
                });

            When(x => x.OrderDto.Partner == PartnerType.PartnerD,
                () =>
                {
                    RuleFor(x => x.OrderDto.OrderItems).Must(x => x.All(s => s.WebsiteDetails == null))
                        .WithMessage("Partner D not support WebSite Product");

                    RuleFor(x => x.OrderDto.OrderItems).Must(x => x.All(s => s.AdWordCampaign != null))
                        .WithMessage("Partner D missing WebSite PaidSearch");

                    RuleFor(x => x.OrderDto.OrderItems).Must(x => x.All(s => s.ProductType == ProductType.PaidProduct))
                        .WithMessage("Partner D product type should be 'PaidProduct'");
                });

            RuleFor(x => x.OrderDto.OrderItems).Must(x => x.All(s =>
                    s.ProductType == ProductType.PaidProduct && s.AdWordCampaign != null ||
                    s.ProductType == ProductType.WebSite && s.WebsiteDetails != null))
                .WithMessage("Conflict between product type and its detail property");
        }
    }
}