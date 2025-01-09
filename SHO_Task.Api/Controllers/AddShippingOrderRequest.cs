using SHO_Task.Application.ShippingOrders;

namespace SHO_Task.Api.Controllers;

public sealed record AddShippingOrderRequest(
    Guid PurchaseOrderId,
    int PalletCount,
    string PriceCurrencyCode,
    IReadOnlyList<AddShippingOrderItemRequest> ShippingOrderItems
    )
{
    public static implicit operator AddShippingOrderCommand(AddShippingOrderRequest request)
    {
        return new AddShippingOrderCommand(
                request.PurchaseOrderId,
                request.PalletCount,
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
