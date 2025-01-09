using SHO_Task.Application.Abstractions.Messaging;

namespace SHO_Task.Application.ShippingOrders;

public sealed record AddShippingOrderCommand(
        Guid PurchaseOrderId,
        int PalletCount,
        IReadOnlyList<ShippingOrderItemCommand> ShippingOrderItems
    ) : ICommand<ShippingOrderCreateCommandResult>;

public sealed record ShippingOrderItemCommand(
        string GoodCode,
        decimal Quantity,
        decimal Price,
        string PriceCurrencyCode
    );
