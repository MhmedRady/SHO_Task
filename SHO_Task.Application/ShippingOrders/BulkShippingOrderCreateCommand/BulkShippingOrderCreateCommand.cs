using SHO_Task.Application.Abstractions.Messaging;
using SHO_Task.Domain.ShippingOrders;

namespace SHO_Task.Application.ShippingOrders;


public sealed record BulkShippingOrderCreateCommand(
    IReadOnlyList<BulkShippingOrderCommand> BulkShippingOrderCommands
    ): ICommand<IReadOnlyList<ShippingOrderCreateCommandResult>>;

public sealed record BulkShippingOrderCommand(
        Guid PurchaseOrderId,
        int PalletCount,
        IEnumerable<BulkShippingOrderItemCreateCommand> PO_Items
    ) : ICommand<IEnumerable<Guid>>;

public sealed record BulkShippingOrderItemCreateCommand(
        string GoodCode,
        //int SerialNumber,
        decimal Quantity,
        decimal Price,
        string PriceCurrencyCode
    );
