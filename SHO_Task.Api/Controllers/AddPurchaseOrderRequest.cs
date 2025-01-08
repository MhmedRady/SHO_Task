using SHO_Task.Application.ShippingOrders;
using SHO_Task.Domain.ShippingOrders;

namespace SHO_Task.Api.Controllers;

public sealed record AddShippingOrderRequest(
    PoNumberGeneratorType PONumberType,
    string PriceCurrencyCode,
    IReadOnlyList<AddShippingOrderItemRequest> ShippingOrderItems
    )
{
    public static implicit operator AddShippingOrderCommand(AddShippingOrderRequest request)
    {
        return new AddShippingOrderCommand(
                request.PONumberType,
                request.ShippingOrderItems.Select(poItem =>
                new ShippingOrderItemCommand(
                    poItem.GoodCode,
                    poItem.Quantity,
                    poItem.Price,
                    request.PriceCurrencyCode
                )
            ).ToArray()
        );
    }
}

public sealed record AddShippingOrderItemRequest(
    string GoodCode,
    int SerialNumber,
    decimal Quantity,
    decimal Price);
