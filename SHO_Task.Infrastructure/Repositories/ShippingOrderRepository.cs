using Microsoft.EntityFrameworkCore;
using SHO_Task.Domain;
using SHO_Task.Domain.ShippingOrders;

namespace SHO_Task.Infrastructure.Repositories;

internal sealed class ShippingOrderRepository :
    Repository<ShippingOrder, ShippingOrderId>, IShippingOrderRepository
{
    public ShippingOrderRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<ShippingOrder?> GetBySHONumber(string SHONumber, CancellationToken cancellationToken)
    {
        return await GetBy(po => string.Equals(SHONumber, po.SHONumber, StringComparison.OrdinalIgnoreCase)).Include(p => p.ShippingOrderItems).FirstOrDefaultAsync(cancellationToken);
    }


}
