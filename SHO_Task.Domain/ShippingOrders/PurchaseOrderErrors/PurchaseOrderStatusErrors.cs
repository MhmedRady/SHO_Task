using SHO_Task.Domain.BuildingBlocks;

namespace SHO_Task.Domain.ShippingOrders;

public static class ShippingOrderStatusErrors
{
    public static readonly Error ShipmentsInPreparationToReadyForPickupRuleUserError = new(
        "Orders.ShipmentsInPreparationToReadyForPickupRule.UserError",
        "The current user does not belong to the required role to move from 'shipments in preparation' to 'shipments ready for pickup'");

    public static readonly Error ShipmentsInPreparationToReadyForPickupRuleHandoverMethodError = new(
        "Orders.ShipmentsInPreparationToReadyForPickupRule.HandoverMethod",
        "The handover method must be pickup to move from 'shipments in preparation' to 'shipments ready for pickup'");
}
