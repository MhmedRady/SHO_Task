﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHO_Task.Application.IntegrationEvents
{
    public record ShippingOrderCreatedIntegrationEvent(Guid ShippingOrderId, Guid PurchaseOrderId);
    public record ShippingOrderClosedIntegrationEvent(Guid ShippingOrderId, Guid PurchaseOrderId);

}
