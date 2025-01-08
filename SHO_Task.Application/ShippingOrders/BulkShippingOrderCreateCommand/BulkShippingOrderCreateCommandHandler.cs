using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SHO_Task.Application.Abstractions.Messaging;
using SHO_Task.Application.Exceptions;
using SHO_Task.Domain;
using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Domain.Common;
using SHO_Task.Domain.Items;
using SHO_Task.Domain.ShippingOrders;
using SHO_Task.Domain.Users;

namespace SHO_Task.Application.ShippingOrders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BulkShippingOrderCreateCommandHandler(
    IShippingOrderRepository _PoRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<BulkShippingOrderCreateCommand,
        IReadOnlyList<ShippingOrderCreateCommandResult>>
{

    public async Task<IReadOnlyList<ShippingOrderCreateCommandResult>> Handle(
        BulkShippingOrderCreateCommand request,
        CancellationToken cancellationToken)
    {
        
        var createdShippingOrderIds = new List<ShippingOrderCreateCommandResult>();
        var createdShippingOrder = new List<ShippingOrder>();
        var errors = new List<ApplicationError>();

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        int poRequestIndex = 1;

        foreach (var poRequest in request.BulkShippingOrderCommands)
        {
            
            try
            {
                ShippingOrderId SHoOrderId = ShippingOrderId.CreateUnique();
                var poItems = poRequest.PO_Items.Select(itemCmd =>
                    CreateOredItem(
                        ShippingOrderId: SHoOrderId,
                        orderItemCommand: itemCmd
                    )
                ).ToArray();

                if (!poItems.Any())
                        errors.Add(BulkShippingOrderCreateCommandErrors.PurchaserItemIsEmpty(poRequestIndex));

                DateTime issueDate = DateTime.Now;

                var po = ShippingOrder.CreateOrderInstance(
                    SHoOrderId,
                    poRequest.PurchaseOrderId,
                    poRequest.PalletCount,
                    poItems
                );

                await _PoRepository.AddAsync(po);
                createdShippingOrderIds.Add(new(po.CreatedAt, po.SHONumber));
                ++poRequestIndex;
            }
            catch (ApplicationFlowException ex)
            {
                errors.Add(new($"{nameof(BulkShippingOrderCreateCommandHandler)} Error Index Number: {poRequestIndex}", ex.Message));
            }
            catch (Exception ex)
            {
                errors.Add(new($"{nameof(BulkShippingOrderCreateCommandHandler)} For adding Index {poRequestIndex} in a Bulk Order Create", $", the Purchaser Error: \n{ex.Message}."));
            }
        }

        if (errors.Any())
        {
            await _unitOfWork.RollbackAsync(cancellationToken);

            throw new ApplicationFlowException(
                errors.Select(e => e)
            );
        }

        try
        {
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception ex) 
        { 
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw new InvalidOperationException(nameof(BulkShippingOrderCreateCommandHandler), ex);
        }

        return createdShippingOrderIds;
    }

    private ShippingOrderItem CreateOredItem(ShippingOrderId ShippingOrderId,
        BulkShippingOrderItemCreateCommand orderItemCommand)
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
