using SHO_Task.Application.Abstractions.Messaging;
using SHO_Task.Application.Exceptions;
using SHO_Task.Domain;
using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Domain.Common;
using SHO_Task.Domain.Items;
using SHO_Task.Domain.ShippingOrders;

namespace SHO_Task.Application.ShippingOrders;

// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class AddShippingOrderItemCommandHandler(
    IShippingOrderRepository _ShippingOrderRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<AddShippingOrderItemCommand, bool>
{
    public async Task<bool> Handle(
        AddShippingOrderItemCommand request,
        CancellationToken cancellationToken)
    {

        var POrder = await _ShippingOrderRepository.GetBySHONumber(request.SHONumber, default)
                        ?? throw new ApplicationFlowException([AddShippingOrderItemCommandErrors.PurchaserNumberNotFound]);

        ShippingOrderItem orderItem = CreateOredItem(POrder.Id, request);

        POrder.AddOrderItem(orderItem);

        return _unitOfWork.SaveChangesAsync().IsCompletedSuccessfully;
    }

    private ShippingOrderItem CreateOredItem(ShippingOrderId ShippingOrderId,
        AddShippingOrderItemCommand orderItemCommand)
    {
        var orderItem = ShippingOrderItem.CreateInstance(
            ShippingOrderId,
            orderItemCommand.GoodCode,
            orderItemCommand.Quantity,
            new Money(
                orderItemCommand.Price,
                Currency.FromCode(orderItemCommand.PriceCurrencyCode))
            );
        return orderItem;
    }
}
