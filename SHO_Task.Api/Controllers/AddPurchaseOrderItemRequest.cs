/*using SHO_Task.Application.ShippingOrders;

namespace SHO_Task.Api.Controllers;

public sealed record AddShippingOrderItemRequest(
        string PoNumber,
        string GoodCode,
        //int SerialNumber,
        decimal Quantity,
        decimal Price,
        string PriceCurrencyCode
    )
{
    public static implicit operator AddShippingOrderItemCommand(AddShippingOrderItemRequest request)
    {
        return new AddShippingOrderItemCommand(
            request.PoNumber,
            request.GoodCode,
            //request.SerialNumber,
            request.Quantity,
            request.Price,
            request.PriceCurrencyCode
        );
    }
}*/