

using SHO_Task.Domain.BuildingBlocks;

namespace SHO_Task.Domain.Users.Events;

public sealed record ShippingOrderCreatedDomainEvent : IDomainEvent
{
    public Guid Id { get; init; }
    public string PONumber { get; init; }
}
