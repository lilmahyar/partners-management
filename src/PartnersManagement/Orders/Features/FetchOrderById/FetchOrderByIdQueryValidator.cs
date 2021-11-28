using FluentValidation;

namespace PartnersManagement.Orders.Features.FetchOrderById
{
    public class FetchOrderByIdQueryValidator : AbstractValidator<FetchOrderByIdQuery>
    {
        public FetchOrderByIdQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0).WithMessage("id should be greater than zero.");
        }
    }
}