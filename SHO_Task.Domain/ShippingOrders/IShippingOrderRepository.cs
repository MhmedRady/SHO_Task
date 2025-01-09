using SHO_Task.Domain.ShippingOrders;

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
