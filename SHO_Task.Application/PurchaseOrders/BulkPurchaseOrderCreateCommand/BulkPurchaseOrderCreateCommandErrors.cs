using SHO_Task.Application.Exceptions;
using SHO_Task.Domain.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHO_Task.Application.ShippingOrders;

public static class BulkShippingOrderCreateCommandErrors
{
    public static readonly ApplicationError PurchaserIdNotFound = new(
        $"{nameof(BulkShippingOrderCreateCommand)}.Purchaser UserId",
        "For adding a Order, the Purchaser UserId must be existing.");

    public static readonly ApplicationError NotFound = new(
        $"{nameof(BulkShippingOrderCreateCommand)}.Purchaser UserId",
        "For adding a Order, the Purchaser UserId must be existing.");

    public static ApplicationError PurchaserItemIsEmpty(int index) => new(
        $"{nameof(AddShippingOrderCommand)}. Index Order { index } Items Count",
        "For adding a Order, the Purchases Order goods must be specified.");
}
