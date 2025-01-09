using SHO_Task.Domain.BuildingBlocks;

namespace SHO_Task.Domain.ShippingOrders;

public static class ShippingOrderErrors
{
    public static readonly Error NotFound = new(
        "ShippingOrder.NotFound",
        "The ShippingOrder was not found");

    public static readonly Error MultipleCurrencyTypes = new(
        "OrderItems.PriceError",
        "The Multiple Price Currency Types");
}
