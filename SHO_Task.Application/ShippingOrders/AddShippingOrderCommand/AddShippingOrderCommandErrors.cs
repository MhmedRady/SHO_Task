using SHO_Task.Application.Exceptions;

namespace SHO_Task.Application.ShippingOrders;

public static class AddShippingOrderCommandErrors
{
    public static readonly ApplicationError PurchaserIdNotFound = new(
        $"{nameof(AddShippingOrderCommand)}.Purchaser UserId",
        "For adding a Order, the Purchaser UserId must be existing.");

    public static readonly ApplicationError PurchaserItemIsEmpty = new(
        $"{nameof(AddShippingOrderCommand)}.Order Items Count",
        "For adding a Order, the Purchases Order goods must be specified.");
}
