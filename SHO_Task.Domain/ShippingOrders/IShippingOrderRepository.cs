using SHO_Task.Domain.ShippingOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace SHO_Task.Domain
{
    public interface IShippingOrderRepository
    {
        Task<ShippingOrder?> GetBySHONumber(string SHONumber, CancellationToken cancellationToken);
        Task AddAsync(ShippingOrder ShippingOrder);
        void Update(ShippingOrder ShippingOrder);
        void Delete(ShippingOrder ShippingOrder);
    }
}
