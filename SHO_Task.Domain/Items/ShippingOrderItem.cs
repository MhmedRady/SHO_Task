using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Domain.Common;
using SHO_Task.Domain.ShippingOrders;

namespace SHO_Task.Domain.Items;

public class ShippingOrderItem : Entity<ItemId>, IAggregateRoot
{

    private ShippingOrderItem(
        ItemId id,
        ShippingOrderId shippingOrderId,
        string goodCode,
        Money price
        ) : base(id)
    {
        ShippingOrderId = shippingOrderId;
        GoodCode = goodCode;
        Price = price;
    }

    private ShippingOrderItem()
    {
    }

    public string GoodCode { get; private set; }
    public int SerialNumber { get; private set; }

    public ShippingOrderId ShippingOrderId { get; private set; }
    public Money Price { get; private set; }
    public decimal Quantity { get; private set; }

    public static ShippingOrderItem CreateInstance(
        ShippingOrderId shippingOrderId,
        string goodCode,
        //int serialNumber,
        decimal quantity,
        Money price
        )
    {
        var SerialNumber = new Random().Next(100000);
        return new ShippingOrderItem(
            ItemId.CreateUnique(),
            shippingOrderId,
            goodCode,
            price
            );
    }
}