namespace SHO_Task.Application.ShippingOrders
{
    public sealed record ShippingOrderCreateCommandResult(DateTime IssueDate, string SHONumber);
}
