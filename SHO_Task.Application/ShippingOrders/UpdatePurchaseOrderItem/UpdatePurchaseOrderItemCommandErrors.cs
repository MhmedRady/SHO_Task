using SHO_Task.Application.Exceptions;

namespace SHO_Task.Application.ShippingOrders;

public static class UpdateShippingOrderItemCommandErrors
{
    public static readonly ValidationError PurchaserIdNotFound = new(
        $"{nameof(ShippingOrderItemCommand)}.Purchaser UserId",
        "For adding a Order, the Purchaser UserId must be existing.");
}
