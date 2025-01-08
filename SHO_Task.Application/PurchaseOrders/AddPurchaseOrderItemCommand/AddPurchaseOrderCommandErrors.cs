using SHO_Task.Application.Exceptions;

namespace SHO_Task.Application.ShippingOrders;

public static class AddShippingOrderItemCommandErrors
{
    public static readonly ApplicationError PurchaserNumberNotFound = new(
        $"{nameof(AddShippingOrderItemCommand)}.Purchaser Order Number",
        "For adding a Order Item, the Purchaser Order is Not exist.");
}
