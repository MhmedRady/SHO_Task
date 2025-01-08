﻿/*using SHO_Task.Application.Abstractions.Messaging;
using SHO_Task.Application.Exceptions;
using SHO_Task.Domain;
using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Domain.Common;
using SHO_Task.Domain.Items;
using SHO_Task.Domain.ShippingOrders;
using SHO_Task.Domain.Users;

namespace SHO_Task.Application.ShippingOrders;

// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class AddShippingOrderItemCommandHandler(
    IShippingOrderRepository _ShippingOrderRepository,
    IUserRepository _userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddShippingOrderCommand, ShippingOrderCreateCommandResult>
{
    public async Task<ShippingOrderCreateCommandResult> Handle(
        AddShippingOrderCommand request,
        CancellationToken cancellationToken)
    {

        var isExistUserId = await _userRepository.GetByIdAsync(UserId.Create(request.PurchaserId))
                ?? throw new ValidationException([AddShippingOrderCommandErrors.PurchaserIdNotFound]);

        ShippingOrderId ShippingOrderId = ShippingOrderId.CreateUnique();

        var ShippingOrder = ShippingOrder.CreateOrderInstance(
                ShippingOrderId,
                UserId.Create(request.PurchaserId),
                DateTime.Now
            );

        ShippingOrderItem orderItem = CreateOredItem(
            ShippingOrderId,
            request.ShippingOrderItem);

        ShippingOrder.AddOrderItem(orderItem);

        _ShippingOrderRepository.Add(ShippingOrder);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ShippingOrder.Id.Value;
    }

    private ShippingOrderItem CreateOredItem(ShippingOrderId ShippingOrderId,
        ShippingOrderItemCommand orderItemCommand)
    {
        var orderItem = ShippingOrderItem.CreateInstance(
            ShippingOrderId,
            orderItemCommand.GoodCode,
            //orderItemCommand.SerialNumber,
            orderItemCommand.Quantity,
            new Money(
                orderItemCommand.Price * orderItemCommand.Quantity,
                Currency.FromCode(orderItemCommand.PriceCurrencyCode))
            );
        return orderItem;
    }
}
*/