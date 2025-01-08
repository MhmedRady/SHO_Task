using SHO_Task.Application.Abstractions.Messaging;

namespace SHO_Task.Application.ShippingOrders;

public sealed record AddShippingOrderItemCommand(
    string SHONumber,
    string GoodCode,
    //int SerialNumber,
    decimal Quantity,
    decimal Price,
    string PriceCurrencyCode
    ) : ICommand<bool>;