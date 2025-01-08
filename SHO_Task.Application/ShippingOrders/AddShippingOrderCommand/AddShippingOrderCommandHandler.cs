using SHO_Task.Application.Abstractions.Messaging;
using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Domain.Users;
using SHO_Task.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHO_Task.Domain.Items;
using SHO_Task.Domain.ShippingOrders;
using SHO_Task.Application.Exceptions;
using SHO_Task.Domain.Common;
using SHO_Task.Application.Behaviors;

namespace SHO_Task.Application.ShippingOrders;

internal class AddShippingOrderCommandHandler(
    ShippingOrderDomainEventDispatcher _dispatcher,
    IShippingOrderRepository _sHORepository,
    IUnitOfWork _unitOfWork) : ICommandHandler< AddShippingOrderCommand, ShippingOrderCreateCommandResult>
{
    public async Task<ShippingOrderCreateCommandResult> Handle(AddShippingOrderCommand request, CancellationToken cancellationToken)
    {

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {

            ShippingOrderId ShippingOrderId = ShippingOrderId.CreateUnique();

            var issueDate = DateTime.Now;

            var poItems = request.ShippingOrderItems.Select(poItem => CreateOredItem(ShippingOrderId, poItem)).ToArray();

            if (!poItems.Any())
                throw new ApplicationFlowException([AddShippingOrderCommandErrors.PurchaserItemIsEmpty]);

            var shippingOrder = ShippingOrder.CreateOrderInstance(
                    ShippingOrderId,
                    request.PurchaseOrderId,
                    request.PalletCount,
                    poItems
                );

            await _sHORepository.AddAsync(shippingOrder);
            await _unitOfWork.CommitAsync(cancellationToken);
            _dispatcher.DispatchDomainEvents(shippingOrder);

            return new ShippingOrderCreateCommandResult(shippingOrder.CreatedAt, shippingOrder.SHONumber);
        }
        catch (Exception ex)
        {
            throw new ApplicationFlowException([new(nameof(AddShippingOrderCommandHandler), ex.Message)]);
        }
    }

    private ShippingOrderItem CreateOredItem(ShippingOrderId ShippingOrderId,
        ShippingOrderItemCommand orderItemCommand)
    {
        var orderItem = ShippingOrderItem.CreateInstance(
            ShippingOrderId,
            orderItemCommand.GoodCode,
            orderItemCommand.Quantity,
            new Money(
                orderItemCommand.Price * orderItemCommand.Quantity,
                Currency.FromCode(orderItemCommand.PriceCurrencyCode))
            );
        return orderItem;
    }
}
