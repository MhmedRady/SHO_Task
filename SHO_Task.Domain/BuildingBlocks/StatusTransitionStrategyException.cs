using SHO_Task.Domain.ShippingOrders;

namespace SHO_Task.Domain.BuildingBlocks;

public class StatusTransitionStrategyException : Exception
{
    public StatusTransitionStrategyException(SHOState status) : base($"No strategy found for status {status}")
    {
    }
}
