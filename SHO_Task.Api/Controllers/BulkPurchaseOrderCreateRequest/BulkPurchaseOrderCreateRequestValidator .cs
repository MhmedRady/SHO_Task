using FluentValidation;

namespace SHO_Task.Api.Controllers;

public class BulkShippingOrderCreateRequestValidator : AbstractValidator<BulkShippingOrderCreateRequest>
{
    public BulkShippingOrderCreateRequestValidator()
    {
        RuleFor(x => x.ShippingOrderRequests)
            .NotEmpty().WithMessage("At least one purchase order is required.");

        RuleForEach(x => x.ShippingOrderRequests)
            .SetValidator(new BulkShippingOrderRequestValidator())
            .WithMessage("Invalid purchase order request.");
    }
}

public class BulkShippingOrderRequestValidator : AbstractValidator<BulkShippingOrderRequest>
{
    public BulkShippingOrderRequestValidator()
    {
        RuleFor(x => x.PoNumberType)
            .IsInEnum().WithMessage("Invalid PO Number Type Nust Be 0 => New Or 1 => Old.");

        RuleFor(x => x.PriceCurrencyCode)
            .NotEmpty().WithMessage("PriceCurrencyCode is required Should be in [EGP, USD, EUR].")
            .Length(3).WithMessage("PriceCurrencyCode must be exactly 3 characters, Should be in [EGP, USD, EUR].") // ISO currency code format
            .Matches("^[A-Z]{3}$").WithMessage("PriceCurrencyCode must be uppercase alphabetical characters, Should be in [EGP, USD, EUR].");

        RuleForEach(x => x.ShippingOrderItems)
            .SetValidator(new BulkShippingOrderItemRequestValidator())
            .WithMessage("Invalid purchase order item.");
    }
}

public class BulkShippingOrderItemRequestValidator : AbstractValidator<BulkShippingOrderItemRequest>
{
    public BulkShippingOrderItemRequestValidator()
    {
        RuleFor(x => x.GoodCode)
            .NotEmpty().WithMessage("GoodCode cannot be empty.")
            .MaximumLength(50).WithMessage("GoodCode must not exceed 50 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
    }
}
