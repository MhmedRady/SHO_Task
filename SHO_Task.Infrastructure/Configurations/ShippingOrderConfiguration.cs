using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SHO_Task.Domain.Common;
using SHO_Task.Domain.Items;
using SHO_Task.Domain.ShippingOrders;
using SHO_Task.Domain.Users;

namespace SHO_Task.Infrastructure.Configurations
{
    internal sealed class ShippingOrderConfiguration : IEntityTypeConfiguration<ShippingOrder>
    {
        public void Configure(EntityTypeBuilder<ShippingOrder> builder)
        {
            ConfigureOrder(builder);
        }

        public static void ConfigureOrder(EntityTypeBuilder<ShippingOrder> builder)
        {
            builder.ToTable("ShippingOrders", "SHO");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
              .ValueGeneratedNever()
              .HasConversion(
                  id => id.Value,
                  value => ShippingOrderId.Create(value));

            /*builder.HasIndex(o => o.PurchaserId);
            builder.Property(i => i.PurchaserId)
                    .ValueGeneratedNever() 
                    .HasConversion(
                        id => id.Value,
                        value => UserId.Create(value));*/

            // Unique index for ShoNumber
            builder.HasIndex(po => po.SHONumber).IsUnique();

            builder.HasIndex(l => l.PurchaseOrderId);

            builder.Property(l => l.PalletCount);
            builder.Property(l => l.DeliveryDate);
            builder.Property(l => l.CreatedAt);


            // OwnsMany for ShippingOrderItems
            builder.OwnsMany(o => o.ShippingOrderItems, ol =>
            {
                ol.ToTable("ShippingItems", "SHO");
                ol.HasKey(i => i.Id);

                // Value object mapping for Id
                ol.Property(i => i.Id)
                  .ValueGeneratedNever()
                  .HasConversion(
                      id => id.Value,
                      value => ItemId.Create(value)
                    );

                // Index on ShippingOrderId
                ol.HasIndex(l => l.ShippingOrderId);

                ol.Property(i => i.ShippingOrderId)
                    .ValueGeneratedNever()
                    .HasConversion(
                        ShippingOrderId => ShippingOrderId.Value,
                        value => ShippingOrderId.Create(value)
                    );

                // Relationship to ShippingOrder
                ol.HasOne<ShippingOrder>()
                  .WithMany(po => po.ShippingOrderItems)
                  .HasForeignKey(i => i.ShippingOrderId);

                // Optional: Soft delete filter
                // ol.HasQueryFilter(q => !q.DeletedAt.HasValue);

                // Properties
                ol.Property(l => l.GoodCode)
                  .IsRequired()
                  .HasMaxLength(50);

                ol.Property(l => l.Quantity)
                  .HasColumnType("decimal(10,2)")
                  .IsRequired();

                ol.Property(l => l.SerialNumber)
                  .IsRequired();

                // Value object mapping for Price (Money)
                ol.OwnsOne(
                    s => s.Price,
                    price =>
                    {
                        price.Property(m => m.Amount)
                             .HasColumnType("decimal(18,2)")
                             .IsRequired();

                        price.Property(m => m.Currency)
                             .HasConversion(
                                 currency => currency.Code,
                                 code => Currency.FromCode(code))
                             .HasMaxLength(3)
                             .IsRequired();
                    });
            });
        }
    }
}
