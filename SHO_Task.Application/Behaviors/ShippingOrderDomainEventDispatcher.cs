using SHO_Task.Application.IntegrationEvents;
using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Domain.ShippingOrders;
using SHO_Task.Domain.Users.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHO_Task.Application.Behaviors
{
    public sealed class ShippingOrderDomainEventDispatcher
    {
        private readonly IRabbitMqPublisher _rabbitMqPublisher;

        public ShippingOrderDomainEventDispatcher(IRabbitMqPublisher rabbitMqPublisher)
        {
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        public void DispatchDomainEvents(ShippingOrder so)
        {
            foreach (var domainEvent in so.GetDomainEvents())
            {
                switch (domainEvent)
                {
                    case ShippingOrderCreatedDomainEvent created:
                        {
                            var ie = new ShippingOrderCreatedIntegrationEvent(
                                created.ShippingOrderId,
                                created.PurchaseOrderId
                            );

                            _rabbitMqPublisher.Publish(
                                ie,
                                exchangeName: "shippingorder.exchange",
                                routingKey: "shipping.created"
                            );
                            break;
                        }
                    case ShippingOrderClosedDomainEvent closed:
                        {
                            var ie = new ShippingOrderClosedIntegrationEvent(
                                closed.ShippingOrderId,
                                closed.PurchaseOrderId
                            );
                            _rabbitMqPublisher.Publish(
                                ie,
                                exchangeName: "shippingorder.exchange",
                                routingKey: "shipping.closed"
                            );
                            break;
                        }
                }
            }
            so.ClearDomainEvents();
        }
    }
}
