

using SHO_Task.Domain.BuildingBlocks;

namespace SHO_Task.Domain.Users.Events;

public sealed record ShippingOrderCreatedDomainEvent : IDomainEvent
{
    public Guid ShippingOrderId { get; init; }
    public Guid PurchaseOrderId {  get; init; }
    public string SHONumber { get; init; }

}

public record ShippingOrderClosedDomainEvent(Guid ShippingOrderId, Guid PurchaseOrderId, string SHONumber) : IDomainEvent;