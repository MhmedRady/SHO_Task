using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHO_Task.Application.IntegrationEvents
{
    namespace SHO_Task.Application.IntegrationEvents
    {
        public record ShippingOrderCreatedIntegrationEvent(Guid ShippingOrderId, Guid PurchaseOrderId, string SHONumber, DateTime DeliveryDate);
        public record ShippingOrderClosedIntegrationEvent(Guid ShippingOrderId, Guid PurchaseOrderId, string SHONumber, DateTime DeliveryDate);

        public class IntegrationEventPublisher
        {
            private readonly RabbitMQPublisher _rabbitMQPublisher;

            public IntegrationEventPublisher()
            {
                _rabbitMQPublisher = new RabbitMQPublisher();
            }

            public void PublishShippingOrderCreated(Guid shippingOrderId, Guid purchaseOrderId, string SHONumber, DateTime deliveryDate)
            {
                var integrationEvent = new ShippingOrderCreatedIntegrationEvent(shippingOrderId, purchaseOrderId, SHONumber, deliveryDate);
                _rabbitMQPublisher.Publish(integrationEvent);
            }

            public void PublishShippingOrderClosed(Guid shippingOrderId, Guid purchaseOrderId, string SHONumber, DateTime deliveryDate)
            {
                var integrationEvent = new ShippingOrderClosedIntegrationEvent(shippingOrderId, purchaseOrderId, SHONumber, deliveryDate);
                _rabbitMQPublisher.Publish(integrationEvent);
            }
        }
    }
}
