using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Domain.Items;
using SHO_Task.Domain.Users.Events;
using System.Collections.ObjectModel;


namespace SHO_Task.Domain.ShippingOrders;

public enum SHOState
{
    Created,
    Closed
}

public sealed class ShippingOrder : Entity<ShippingOrderId>, IAggregateRoot
{
    private readonly List<ShippingOrderItem> _items = new();

    private ShippingOrder() { }

    private ShippingOrder(ShippingOrderId shippingOrderId, Guid purchaseOrderId,
            int palletCount)
    {
        Id = shippingOrderId;
        PurchaseOrderId = purchaseOrderId;
        PalletCount = palletCount;
    }

    public string SHONumber { get; private set; }
    /// <summary>
    /// The ID (or number) of the PO being fulfilled. We store this as a reference.
    /// </summary>  
    public Guid PurchaseOrderId { get; private set; }

    /// <summary>
    /// The current state of the SHO (e.g., Created, Closed).
    /// </summary>
    public SHOState State { get; private set; }

    /// <summary>
    /// How many pallets are used to deliver these goods.
    /// </summary>
    public int PalletCount { get; private set; }
    /// <summary>
    /// The date when the goods will be delivered to the purchaser.
    /// </summary>
    public DateTime DeliveryDate { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public ReadOnlyCollection<ShippingOrderItem> ShippingOrderItems => _items.AsReadOnly();

    public static ShippingOrder CreateOrderInstance(
            ShippingOrderId shippingOrderId,
            Guid PurchaseOrderId,
            int PalletCount,
            IEnumerable<ShippingOrderItem> shippingOrderItems
        )
    {
        ShippingOrder sho = new ShippingOrder(shippingOrderId, PurchaseOrderId, PalletCount);

        DateTime IssueDate = DateTime.UtcNow;

        sho.State = SHOState.Created;
        sho.CreatedAt = IssueDate;
        sho.SHONumber = CreateSHONumber(IssueDate);

        foreach (var poItem in shippingOrderItems)
        {
            sho.AddOrderItem(poItem);
        }

        sho.RaiseDomainEvent(new ShippingOrderCreatedDomainEvent { ShippingOrderId = shippingOrderId.Value, SHONumber = sho.SHONumber, PurchaseOrderId = sho.PurchaseOrderId, DeliveryDate = IssueDate });

        return sho;
    }

    public static string CreateSHONumber(DateTime createdDate)
    {
        var timeStaps = Int64.Parse(createdDate.ToString("yyyyMMddHHmmss")) + new Random().Next(1000);
        return $"PO-{timeStaps}";
    }

    public void AddOrderItem(ShippingOrderItem shippingOrderItem)
    {
        _items.Add(shippingOrderItem);
    }

    public void IsClosedState()
    {
        if (State == SHOState.Created)
        {
            State = SHOState.Closed;
        }

        this.RaiseDomainEvent(new ShippingOrderClosedDomainEvent(ShippingOrderId: Id.Value, PurchaseOrderId: PurchaseOrderId, SHONumber: SHONumber, DeliveryDate));
    }
}
