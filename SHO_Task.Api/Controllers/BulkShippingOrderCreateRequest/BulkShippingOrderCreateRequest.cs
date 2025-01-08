using SHO_Task.Application.ShippingOrders;
using SHO_Task.Domain.ShippingOrders;

namespace SHO_Task.Api.Controllers;

public sealed record BulkShippingOrderCreateRequest(

    IReadOnlyList<BulkShippingOrderRequest> ShippingOrderRequests
    )
{
    public static implicit operator BulkShippingOrderCreateCommand(BulkShippingOrderCreateRequest request)
    {
        return new BulkShippingOrderCreateCommand(
                request.ShippingOrderRequests.Select(poReuest =>
                        new BulkShippingOrderCommand(
                            poReuest.PurchaseOrderId,
                            poReuest.PalletCount,
                            PO_Items: poReuest.ShippingOrderItems.Select(poItemRequest =>
                                    new BulkShippingOrderItemCreateCommand
                                    (
                                        poItemRequest.GoodCode,
                                        poItemRequest.Quantity,
                                        poItemRequest.Price,
                                        "EGP"
                                    )
                                )
                            )
                    ).ToArray()
            );
    }
}

public sealed record BulkShippingOrderRequest(
        Guid PurchaseOrderId,
        int PalletCount,
        IEnumerable<BulkShippingOrderItemRequest> ShippingOrderItems
    );

public sealed record BulkShippingOrderItemRequest(
    string GoodCode,
    decimal Quantity,
    decimal Price);
