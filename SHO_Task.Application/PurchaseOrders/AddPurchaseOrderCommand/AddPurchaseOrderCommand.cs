using SHO_Task.Application.Abstractions.Messaging;
using SHO_Task.Domain.ShippingOrders;
using SHO_Task.Domain.Users;

namespace SHO_Task.Application.ShippingOrders;

public sealed record AddShippingOrderCommand(
        SHONumberGeneratorType SHONumberType,
        IReadOnlyList<ShippingOrderItemCommand> ShippingOrderItems
    ) : ICommand<ShippingOrderCreateCommandResult>;

public sealed record ShippingOrderItemCommand(
        string GoodCode,
        decimal Quantity,
        decimal Price,
        string PriceCurrencyCode
    );
