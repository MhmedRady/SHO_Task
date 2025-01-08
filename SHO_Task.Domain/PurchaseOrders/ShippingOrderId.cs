using SHO_Task.Domain.BuildingBlocks;

namespace SHO_Task.Domain.ShippingOrders;

public sealed record ShippingOrderId : ValueObject
{
    private ShippingOrderId(Guid value)
    {
        Value = value;
    }

    private ShippingOrderId() { }
    public Guid Value { get; }

    public static ShippingOrderId CreateUnique()
    {
        return new ShippingOrderId(Guid.NewGuid());
    }

    public static ShippingOrderId Create(Guid value)
    {
        return new ShippingOrderId(value);
    }
}
